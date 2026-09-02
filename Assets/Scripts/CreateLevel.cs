using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class CreateLevel : MonoBehaviour
{
    [SerializeField]
    PokemonList pokemonList = new PokemonList();
    [SerializeField]
    HttpRequestHandler handler = new HttpRequestHandler();
    [SerializeField]
    GameObject pokeCardObj;
    int pokeCount = 0;

    void Start()
    {
        StartCoroutine(GetPokemonList());
    }

    IEnumerator GetPokemonList()
    {
        for(int i = 1; i <= 9; i++)
        {
            handler.method = "GET";
            if (i == 1)
            {
                yield return StartCoroutine(handler.ExecuteRequest(SD.baseURL));
            }
            else
            {
                yield return StartCoroutine(handler.ExecuteRequest(pokemonList.next));
            }

            pokemonList = new PokemonList();
            Debug.Log("Result: " + handler.result);
            pokemonList = JsonUtility.FromJson<PokemonList>(handler.result);
            StartCoroutine(populateBoard());
            Debug.Log("Iteration: " + i);
        }
    }

    IEnumerator populateBoard()
    {
        if(pokeCount < 180)
        {
            foreach (pokeObj pokemon in pokemonList.results)
            {
                if (pokeCount < 180)
                {
                    GameObject PokemonObj;
                PokemonObj = Instantiate(pokeCardObj, transform.position, transform.rotation, transform);
                PokemonObj.GetComponent<PokeCardHandler>().name = pokemon.name;
                PokemonObj.GetComponent<PokeCardHandler>().url = pokemon.url;
                pokeCount++;
                Debug.Log("Pokemon Count: " + pokeCount);
                yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        else
        {
            yield return new WaitForSeconds(0.05f);
        }
    }
}
