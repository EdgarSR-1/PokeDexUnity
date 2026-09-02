using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateLevel : MonoBehaviour
{
    [SerializeField]
    PokemonList pokemonList = new PokemonList();

    [SerializeField]
    HttpRequestHandler handler = new HttpRequestHandler();

    [SerializeField]
    GameObject pokeCardObj;

    // Botón para ir a la página anterior
    [SerializeField]
    Button leftButton;

    // Botón para ir a la página siguiente
    [SerializeField]
    Button rightButton;

    // Página actual
    int currentPage = 0;

    // Pokémon que aparecerán en cada página
    const int pokemonPerPage = 40;

    // Número total de páginas
    int totalPages = 0;

    // Evita que el usuario cambie de página
    // mientras se está descargando otra
    bool isLoading = false;

    // Guarda las tarjetas creadas actualmente
    // para poder destruirlas al cambiar de página
    List<GameObject> currentCards = new List<GameObject>();


    void Start()
    {
        // Conectar los botones con sus funciones
        leftButton.onClick.AddListener(PreviousPage);
        rightButton.onClick.AddListener(NextPage);

        // Cargar la primera página
        StartCoroutine(LoadPokemonPage());
    }


    public void NextPage()
    {
        // No permitir cambiar mientras está cargando
        if (isLoading)
            return;

        // Solo avanzar si no estamos en la última página
        if (currentPage < totalPages - 1)
        {
            currentPage++;

            StartCoroutine(LoadPokemonPage());
        }
    }


    public void PreviousPage()
    {
        // No permitir cambiar mientras está cargando
        if (isLoading)
            return;

        // Solo retroceder si no estamos en la primera página
        if (currentPage > 0)
        {
            currentPage--;

            StartCoroutine(LoadPokemonPage());
        }
    }


    IEnumerator LoadPokemonPage()
    {
        isLoading = true;

        // Desactivar botones mientras carga
        leftButton.interactable = false;
        rightButton.interactable = false;


        // Eliminar los Pokémon de la página anterior
        ClearCurrentPage();


        // Calcular desde qué Pokémon empieza esta página
        int offset = currentPage * pokemonPerPage;


        // Crear URL
        string pageURL =
            SD.baseURL +
            "?limit=" +
            pokemonPerPage +
            "&offset=" +
            offset;


        handler.method = "GET";


        // Descargar la lista de los 40 Pokémon
        yield return StartCoroutine(
            handler.ExecuteRequest(pageURL)
        );


        Debug.Log("Result: " + handler.result);


        // Convertir JSON a PokemonList
        pokemonList =
            JsonUtility.FromJson<PokemonList>(
                handler.result
            );


        // Calcular cuántas páginas existen
        totalPages =
            Mathf.CeilToInt(
                (float)pokemonList.count /
                pokemonPerPage
            );


        // Crear las tarjetas de los 40 Pokémon
        yield return StartCoroutine(PopulateBoard());


        // Actualizar botones
        UpdateButtons();


        isLoading = false;
    }


    IEnumerator PopulateBoard()
    {
        foreach (pokeObj pokemon in pokemonList.results)
        {
            // Crear tarjeta
            GameObject PokemonObj =
                Instantiate(
                    pokeCardObj,
                    transform.position,
                    transform.rotation,
                    transform
                );


            // Obtener el script de la tarjeta
            PokeCardHandler cardHandler =
                PokemonObj.GetComponent<PokeCardHandler>();


            // AQUÍ mantenemos "name" como lo tienes actualmente
            cardHandler.name = pokemon.name;

            cardHandler.url = pokemon.url;


            // Guardar la tarjeta para destruirla
            // cuando cambiemos de página
            currentCards.Add(PokemonObj);


            Debug.Log(
                "Pokemon creado: " + pokemon.name
            );


            // Esperar un frame antes de crear el siguiente
            yield return null;
        }
    }


    void ClearCurrentPage()
    {
        foreach (GameObject card in currentCards)
        {
            if (card != null)
            {
                Destroy(card);
            }
        }


        currentCards.Clear();
    }


    void UpdateButtons()
    {
        // Botón izquierdo solo funciona
        // si no estamos en la primera página
        leftButton.interactable =
            currentPage > 0;


        // Botón derecho solo funciona
        // si no estamos en la última página
        rightButton.interactable =
            currentPage < totalPages - 1;
    }
}