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

    [SerializeField]
    Button leftButton;

    [SerializeField]
    Button rightButton;

    int currentPage = 0;

    const int pokemonPerPage = 40;

    int totalPages = 0;

    bool isLoading = false;

    List<GameObject> currentCards = new List<GameObject>();


    void Start()
    {
        leftButton.onClick.AddListener(PreviousPage);
        rightButton.onClick.AddListener(NextPage);

        StartCoroutine(LoadPokemonPage());
    }


    public void NextPage()
    {
        if (isLoading)
            return;

        if (currentPage < totalPages - 1)
        {
            currentPage++;

            StartCoroutine(LoadPokemonPage());
        }
    }


    public void PreviousPage()
    {
        if (isLoading)
            return;

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

        ClearCurrentPage();

        int offset = currentPage * pokemonPerPage;

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

        pokemonList =
            JsonUtility.FromJson<PokemonList>(
                handler.result
            );

        totalPages =
            Mathf.CeilToInt(
                (float)pokemonList.count /
                pokemonPerPage
            );

        yield return StartCoroutine(PopulateBoard());


        UpdateButtons();


        isLoading = false;
    }


    IEnumerator PopulateBoard()
    {
        foreach (pokeObj pokemon in pokemonList.results)
        {
            GameObject PokemonObj =
                Instantiate(
                    pokeCardObj,
                    transform.position,
                    transform.rotation,
                    transform
                );

            PokeCardHandler cardHandler =
                PokemonObj.GetComponent<PokeCardHandler>();

            cardHandler.name = pokemon.name;

            cardHandler.url = pokemon.url;

            currentCards.Add(PokemonObj);


            Debug.Log(
                "Pokemon creado: " + pokemon.name
            );

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
        leftButton.interactable =
            currentPage > 0;
        rightButton.interactable =
            currentPage < totalPages - 1;
    }
}