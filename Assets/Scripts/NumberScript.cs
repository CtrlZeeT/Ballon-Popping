using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NumberScript : MonoBehaviour
{
    public AudioSource audioSource;
    public string audioFolder = "Number";
    public void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Sound");
        SoundManager.Instance.backGroundMusic.volume = PlayerPrefs.GetFloat("Sound") / 2;
    }
    public void Back()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void OnButtonClick()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        string buttonName;
        if (clickedButton != null)
        {
            buttonName = clickedButton.name;
            PlayMusic(buttonName);
        }
        else
        {
            Debug.Log("Error clicked button not found");
        }
    }
    public void PlayMusic(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>(audioFolder + "/" + clipName);

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Audio clip not found: " + audioFolder + "/" + clipName);
        }
    }

}
