using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharUpload : MonoBehaviour
{
    private int i;
    public GameObject[] AllCharacters;

    private void Start()
    {
        i = PlayerPrefs.GetInt("CurrentCharacter");
        AllCharacters[i].SetActive(true);
    }
}
