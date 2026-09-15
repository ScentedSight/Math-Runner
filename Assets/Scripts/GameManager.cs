using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameOver = false;
    public static int levelDuration;
    public static int score;
    public static int highScore;
    public static int difficultyScaling = 1;
    public static SceneManager scenes;
    public static int currentLevel = 1;
    public DialogueManager dialogueManager;
    private float time;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SceneManager.LoadScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == false)
        {    
            UpdateScore();
        }
    }

    public static void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0; //totally pause the game
    }

    void UpdateScore()
    {
        time += Time.deltaTime;
        score = Mathf.RoundToInt(time * 1000);
    }
}
