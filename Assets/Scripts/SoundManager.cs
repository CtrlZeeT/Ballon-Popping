using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public Slider soundGlider;
    public Slider musicGlider;
    public AudioSource backGroundMusic;
    public AudioSource gameMusic;
    public AudioSource balloonPopSound;
    public AudioSource balloonSquishSound;

    public TextMeshProUGUI alphabetHScore;
    public TextMeshProUGUI numberHScore;
    public TextMeshProUGUI combineHScore;

    // Start is called before the first frame update
    public void Start()
    {
        musicGlider.value = PlayerPrefs.GetFloat("Music", 1f);
        soundGlider.value = PlayerPrefs.GetFloat("Sound", 1f);

        alphabetHScore.text = PlayerPrefs.GetInt("Alphabet", 0).ToString();
        numberHScore.text = PlayerPrefs.GetInt("Number", 0).ToString();
        combineHScore.text = PlayerPrefs.GetInt("Combine", 0).ToString();

        backGroundMusic.volume = PlayerPrefs.GetFloat("Music");
        gameMusic.volume = PlayerPrefs.GetFloat("Music");

        balloonPopSound.volume = PlayerPrefs.GetFloat("Sound");
        balloonSquishSound.volume = PlayerPrefs.GetFloat("Sound");

        backGroundMusic.Play();
    }

    void Awake()
    {
        //Singleton method
        if (Instance == null)
        {
            //First run, set the instance
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (Instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("Music", musicGlider.value);
        backGroundMusic.volume = PlayerPrefs.GetFloat("Music");
        gameMusic.volume = PlayerPrefs.GetFloat("Music");

        PlayerPrefs.SetFloat("Sound", soundGlider.value);
        balloonPopSound.volume = PlayerPrefs.GetFloat("Sound");
        balloonSquishSound.volume = PlayerPrefs.GetFloat("Sound");
    }
}
