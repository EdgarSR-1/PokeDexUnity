using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class PokeCardHandler : MonoBehaviour
{
    public string name;
    public string url;
    public RawImage pokeImage;
    public TextMeshProUGUI text;

    [SerializeField]
    private PokeCard card;

    HttpRequestHandler handler = new HttpRequestHandler();

    void Start()
    {
        pokeImage = GetComponent<RawImage>();
        StartCoroutine(GetPokemon());
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    IEnumerator GetPokemon()
    {
        handler.method = "GET";
        yield return StartCoroutine(handler.ExecuteRequest(url));
        Debug.Log("Result: " + handler.result);
        card = JsonUtility.FromJson<PokeCard>(handler.result);
        if(card.sprites.front_default != null)
        {
            StartCoroutine(addImage());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator addImage()
    {
        UnityWebRequest spriteRequest = UnityWebRequestTexture.GetTexture(card.sprites.front_default);
        yield return spriteRequest.SendWebRequest();
        if (spriteRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(spriteRequest.error);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Sprite request success: " + spriteRequest.responseCode);
            pokeImage.texture = ((DownloadHandlerTexture)spriteRequest.downloadHandler).texture;
            text.text = name;
        }
    }
}
