using TMPro;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject[] balloonPrefab;
    public float spawnRate;
    public float nextSpawnTime;
    public int spawnCounter = 0;

    void Start()
    {
        nextSpawnTime = spawnRate;
    }
    void Update()
    {
        if (Time.time >= nextSpawnTime && GameManager.Instance.gameEnded == false)
        {
            SpawnBalloon();
            nextSpawnTime = Time.time + spawnRate;
        }
    }
    void SpawnBalloon()
    {
        float randomX = Random.Range(-2.15f, 2.15f);
        Vector2 spawnPosition = new(randomX, -6f);
        GameObject newBalloon = Instantiate(balloonPrefab[Random.Range(0, balloonPrefab.Length)], spawnPosition, Quaternion.identity);
        spawnCounter++;
        SetupBalloon(newBalloon);
    }
    private void SetupBalloon(GameObject balloon)
    {
        BalloonController balloonController = balloon.GetComponent<BalloonController>();
        if (balloonController != null)
        {
            balloonController.upSpeed = 0.045f;
            if (spawnCounter > 2)
            {
                if (GameManager.Instance.level == "Alphabet") balloonController.character = GameManager.Instance.targetedCharacter;
                if (GameManager.Instance.level == "Number")
                {
                    balloonController.character = System.Convert.ToChar(GameManager.Instance.randomNumber + 48);
                    balloonController.numberOfPop = GameManager.Instance.randomNumber;
                }
                if (GameManager.Instance.level == "Combine") balloonController.character = GameManager.Instance.targetedCharacter;
                balloonController.characterTMP.text = balloonController.character.ToString();
                spawnCounter = 0;
            }
            else
            {
                if (GameManager.Instance.level == "Alphabet") balloonController.character = GameManager.Instance.GetRandomLetterCharacter();
                if (GameManager.Instance.level == "Number")
                {
                    int temp = GameManager.Instance.GetRandomNumber();
                    balloonController.character = System.Convert.ToChar(temp + 48);
                    balloonController.numberOfPop = temp;
                }
                if (GameManager.Instance.level == "Combine") balloonController.character = GameManager.Instance.GetRandomLetterCharacter();
                balloonController.characterTMP.text = balloonController.character.ToString();
            }
        }

        TextMeshProUGUI textMesh = balloon.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = balloonController.character.ToString();
        }
    }
}
