using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI labelText;

    public char targetedCharacter;
    public int randomNumber;
    public float gameDuration;
    public bool gameEnded;
    public string level;
    public GameObject gameOver;

    private AudioSource gameMusic;
    private int score;
    private float elapsedTime;

    private void Start()
    {
        gameEnded = false;
        gameDuration = 60f;
        score = 0;
        elapsedTime = 0f;
        level = PlayerPrefs.GetString("Level");
        SoundManager.Instance.backGroundMusic.Stop();
        SoundManager.Instance.gameMusic.Play();

        switch (level)
        {
            case "Alphabet":
            case "Combine":
                {
                    targetedCharacter = GetRandomLetterCharacter();
                    labelText.text = "Letter: " + targetedCharacter;
                    break;
                }
            case "Number":
                {
                    randomNumber = GetRandomNumber();
                    targetedCharacter = System.Convert.ToChar(randomNumber + 48);
                    labelText.text = "Number: " + targetedCharacter;
                    break;
                }
            default: break;
        }
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

    void Update()
    {
        if (!gameEnded)
        {
            elapsedTime += Time.deltaTime;
            int remainingTime = Mathf.CeilToInt(gameDuration - elapsedTime);
            timerText.text = "Timer: " + remainingTime;
            if (remainingTime <= 0)
            {
                EndGame();
            }
        }
    }
    void EndGame()
    {
        gameEnded = true;
        timerText.text = "Timer: 0";
        gameOver.SetActive(true);
        if (PlayerPrefs.GetInt(level, 0) < score) PlayerPrefs.SetInt(level, score);
    }
    public void AddScore(int points, char character)
    {
        if (character == targetedCharacter)
        {
            score += points;
            scoreText.text = score.ToString();
        }
    }
    public char GetRandomLetterCharacter()
    {
        return (char)('A' + Random.Range(0, 26));
    }
    public int GetRandomNumber()
    {
        return Random.Range(1, 10);
    }
    public void Retry()
    {
        SceneManager.LoadSceneAsync("GameScreen");

    }
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        Destroy(SoundManager.Instance.gameObject);
        SoundManager.Instance.Start();
    }
}
