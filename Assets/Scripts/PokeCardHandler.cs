using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PokeCardHandler : MonoBehaviour
{
public string name;
public string url;

public RawImage pokeImage;
public TextMeshProUGUI text;

[SerializeField]
private PokeCard card;

private HttpRequestHandler handler =
    new HttpRequestHandler();

private Button button;


void Start()
{
    pokeImage = GetComponent<RawImage>();

    text =
        GetComponentInChildren<TextMeshProUGUI>();

    button = GetComponent<Button>();

    button.onClick.AddListener(OpenDetails);

    StartCoroutine(GetPokemon());
}


IEnumerator GetPokemon()
{
    handler.method = "GET";

    yield return StartCoroutine(
        handler.ExecuteRequest(url)
    );

    Debug.Log("Result: " + handler.result);

    card =
        JsonUtility.FromJson<PokeCard>(
            handler.result
        );

    if (card != null &&
        card.sprites != null &&
        card.sprites.front_default != null)
    {
        StartCoroutine(AddImage());
    }
    else
    {
        Destroy(gameObject);
    }
}


IEnumerator AddImage()
{
    UnityWebRequest spriteRequest =
        UnityWebRequestTexture.GetTexture(
            card.sprites.front_default
        );

    yield return spriteRequest.SendWebRequest();

    if (spriteRequest.result !=
        UnityWebRequest.Result.Success)
    {
        Debug.Log(spriteRequest.error);

        Destroy(gameObject);
    }
    else
    {
        Debug.Log(
            "Sprite request success: " +
            spriteRequest.responseCode
        );

        Texture2D texture =
        DownloadHandlerTexture.GetContent(spriteRequest);

        texture.filterMode = FilterMode.Point;

        pokeImage.texture = texture;

        text.text = name;
    }
}


void OpenDetails()
{
    if (card != null)
    {
        PokemonDetailsPanel.Instance
            .ShowPokemon(card);
    }
}

}
