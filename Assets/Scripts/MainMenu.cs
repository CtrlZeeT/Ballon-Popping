using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame_Combine()
    {
        PlayerPrefs.SetString("Level", "Combine");
        SceneManager.LoadSceneAsync("GameScreen");
    }
    public void PlayGame_Alphabet()
    {
        PlayerPrefs.SetString("Level", "Alphabet");
        SceneManager.LoadSceneAsync("GameScreen");
    }
    public void PlayGame_Number()
    {
        PlayerPrefs.SetString("Level", "Number");
        SceneManager.LoadSceneAsync("GameScreen");
    }
    public void Open_Alphabet()
    {
        SceneManager.LoadSceneAsync("Alphabet");
    }
    public void Open_Number()
    {
        SceneManager.LoadSceneAsync("Number");
    }
}
