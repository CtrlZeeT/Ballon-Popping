using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BalloonController : MonoBehaviour
{
    public float upSpeed;
    public TextMeshProUGUI characterTMP;
    public char character;
    public bool isPopped;
    public int numberOfPop;
    public int score;
    public Animator animator;
    public AudioSource popSound, squishSound;

    private float swaySpeed;
    private float elapsedTime = 1.5f;
    private float interval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        popSound = SoundManager.Instance.balloonPopSound;
        squishSound = SoundManager.Instance.balloonSquishSound;

        characterTMP = GetComponentInChildren<TextMeshProUGUI>();
        //character = GetRandomLetterCharacter();
        isPopped = false;

        switch (GameManager.Instance.level)
        {

            case "Alphabet":
                {

                    numberOfPop = 1;
                    score = 1;
                    break;
                }
            case "Number":
                {
                    numberOfPop = 1;
                    score = 1;
                    break;
                }
            case "Combine":
                {
                    numberOfPop = GameManager.Instance.GetRandomNumber();
                    score = numberOfPop;
                    break;
                }
            default: break;
        } 
            
        if (characterTMP == null)
        {
            Debug.LogError("No TextMeshProUGUI component found on balloon!");
        }
        else
        {
            characterTMP.text = character.ToString(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 12f)
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameEnded == false)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= interval)
            {
                swaySpeed = Random.Range(-0.035f, 0.035f);
                // Reset the timer
                elapsedTime = 0f;
            }
            if (transform.position.x <= -2.15f || transform.position.x >= 2.15f)
            {
                swaySpeed *= -1;
                elapsedTime = 0f;
            }
            transform.Translate(swaySpeed, upSpeed, 0);
        }
        else
        {
            transform.Translate(0, 0, 0);
            this.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        numberOfPop--;
        if (numberOfPop > 0) squishSound.Play();
        if (numberOfPop == 0 && isPopped == false)
        {
            GameManager.Instance.AddScore(score, character);
            squishSound.Stop();
            popSound.Play();
            animator.SetBool("Destroy", true);
            Destroy(gameObject, 0.3f);
        }
    }
}
