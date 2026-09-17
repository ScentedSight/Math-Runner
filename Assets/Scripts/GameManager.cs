using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public float levelDuration = 10f;
    public int score;
    public int highScore;
    public int currentLevel = 1;
    private float time;

    public DialogueManager dialogueManager;
    private SaveManager saveManager;
    public PlayerController player;
    public MenuManager gameUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveManager saveManager = GetComponent<SaveManager>();
        highScore = saveManager.LoadHighScore();
        gameUI.scores.text = $"Highscore: {highScore}\nScore: {score}";
        gameUI.StartScreen();
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

    public void StartGame()
    {
        Time.timeScale = 1f;
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

    public void GameOver()
    {
        gameOver = true;
        if (score > highScore)
        {
            highScore = score;
            saveManager.SaveHighScore(highScore);
        }
        gameUI.EndScreen();
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
