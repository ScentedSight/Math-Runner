using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameOver = false;
    public static float levelDuration = 10f;
    public static int score;
    public static int highScore;
    public static int currentLevel = 1;
    private float time;
    public DialogueManager dialogueManager;
    private SaveManager saveManager;
    public PlayerController player;
    public SceneManager scenes;
    public MenuManager gameUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SceneManager.LoadScene();
        saveManager = GetComponent<SaveManager>();

        int randomStart = Random.Range(0, 2);
        if (randomStart == 0)
        {
            player.DashLeft();
        }
        else
        {
            player.DashRight();
        }

        StartCoroutine(AnswerTimer(levelDuration));
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == false)
        {    
            time += Time.deltaTime;
            UpdateScore();
        }
    }

    public void GameOver()
    {
        gameOver = true;
        saveManager.SaveHighScore(score);
        Time.timeScale = 0; //totally pause the game
    }

    private void UpdateScore()
    {
        score = Mathf.RoundToInt(time * 1000);
    }

    private bool CheckAnswer()
    {
        if (dialogueManager.randomSide == 0 && player.transform.position.x > 0)
        {
            return true;
        }

        if (dialogueManager.randomSide == 1 && player.transform.position.x < 0)
        {
            return true;
        }

        return false;
    }

    private void CorrectAnswer()
    {
        currentLevel++;
        SetLevelDuration();
        StartCoroutine(AnswerTimer(levelDuration));
    }

    IEnumerator AnswerTimer(float seconds)
    {
        dialogueManager.GenerateQuestion();
        dialogueManager.DisplayAnswers();

        yield return new WaitForSeconds(seconds);
        if (CheckAnswer())
        {
            CorrectAnswer();
        }
        else
        {
            GameOver();
        }
    }

    private void SetLevelDuration()
    {
        if (currentLevel > 30)
        {
            levelDuration = 5f;
            return;
        }

        int levelWithinBracket = (currentLevel - 1) % 10; //Find which level we're at within the current 10-level bracket

        float progress = levelWithinBracket / 9f; //Convert 0–9 into 0.0–1.0

        levelDuration = Mathf.Lerp(10f, 4f, progress); //Mathf.Lerp() works conveniently with a value between 0 and 1
    }
}
