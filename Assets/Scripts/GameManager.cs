using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public bool gameStarted = false;
    public float levelDuration = 10f;
    public int score;
    public int highScore;
    public int currentLevel = 1;
    private float time;

    public DialogueManager dialogueManager;
    private SaveManager saveManager;
    public PlayerController player;
    public MenuManager gameUI;
    public SpawnManager spawnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveManager = GetComponent<SaveManager>();
        highScore = saveManager.LoadHighScore();
        gameUI.scores.text = $"Highscore: {highScore}"; //Set highscore text
        gameUI.StartScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && gameStarted)
        {    
            time += Time.deltaTime;
            UpdateScore();
            gameUI.scores.text = $"Highscore: {highScore}\nScore: {score}\nLives X {player.health}"; //Set UI info
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        gameStarted = true;
        int randomStart = Random.Range(0, 2);
        //Place player in either lane when game starts
        if (randomStart == 0)
        {
            player.DashLeft();
        }
        else
        {
            player.DashRight();
        }
        spawnManager.StartSpawning(); //Start spawning barricade
        StartCoroutine(AnswerTimer(levelDuration)); //Start question
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
        if (dialogueManager.randomSide == player.currentLane)
        {
            return true;
        }
        
        return false;
    }

    private void CorrectAnswer()
    {
        currentLevel++;
        SetLevelDuration();
        spawnManager.SetSpawnRate(currentLevel);
        StartCoroutine(AnswerTimer(levelDuration)); //Repeat questionnaire 
    }

    IEnumerator AnswerTimer(float seconds)
    {
        dialogueManager.GenerateQuestion();
        dialogueManager.DisplayAnswers(spawnManager.SpawnQuestions());

        //Update progress bar
        float timeRemaining = seconds;
        gameUI.progressBar.maxValue = seconds;
        gameUI.progressBar.value = seconds;

        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            gameUI.progressBar.value = timeRemaining;

            yield return null; //Wait for next frame
        }

        gameUI.progressBar.value = 0; //Reset progress bar to 0

        if (CheckAnswer())
        {
            CorrectAnswer();
        }
        else if (!CheckAnswer() && player.health < 2)
        {
            GameOver();
        }
        else if (!CheckAnswer() && player.health > 1)
        {
            player.health --;
            CorrectAnswer(); //Repeat questionnaire cycle
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
