using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class PokemonDetailsPanel : MonoBehaviour
{
    public static PokemonDetailsPanel Instance;

    [Header("UI")]
    [SerializeField] private RawImage pokemonImagen;
    [SerializeField] private TextMeshProUGUI pokemonNombre;
    [SerializeField] private TextMeshProUGUI pokemonID;
    [SerializeField] private TextMeshProUGUI pokemonAltura;
    [SerializeField] private TextMeshProUGUI pokemonPeso;
    [SerializeField] private TextMeshProUGUI pokemonTipo;

    void Awake()
    {
        Instance = this;

        gameObject.SetActive(false);
    }

    public void ShowPokemon(PokeCard card)
    {
        gameObject.SetActive(true);

        pokemonNombre.text = card.name;
        pokemonID.text = "ID: " + card.id;
        pokemonAltura.text = "Height: " + card.height;
        pokemonPeso.text = "Weight: " + card.weight;

        ShowTypes(card);

        StartCoroutine(LoadImage(card.sprites.front_default));
    }

    void ShowTypes(PokeCard card)
    {
        StringBuilder typesText = new StringBuilder();

        foreach (TypeElement type in card.types)
        {
            typesText.Append(type.type.name);

            typesText.Append("\n");
        }

        pokemonTipo.text = typesText.ToString();
    }

    IEnumerator LoadImage(string imageURL)
    {
        UnityWebRequest request =
            UnityWebRequestTexture.GetTexture(imageURL);

        yield return request.SendWebRequest();

        if (request.result ==
            UnityWebRequest.Result.Success)
        {
            Texture2D texture =
                DownloadHandlerTexture.GetContent(request);
            
            texture.filterMode = FilterMode.Point;

            pokemonImagen.texture = texture;
        }
        else
        {
            Debug.Log(request.error);
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
