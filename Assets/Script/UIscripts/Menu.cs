using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play()
    {
        SceneManager.LoadScene(2);
    }
     public void Options()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    public void Quite()
    {
        Application.Quit();
    }
}
