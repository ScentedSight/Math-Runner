using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button unpauseButton;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameManager gameManager;
    public Slider progressBar;
    public TMP_Text scores;

    public void StartScreen()
    {
        Time.timeScale = 0f;
        startScreen.SetActive(true);
        startButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    public void PauseScreen()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        unpauseButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public void EndScreen()
    {
        Time.timeScale = 0f;
        endScreen.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameManager.StartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        gameManager.StartGame();
        pauseButton.gameObject.SetActive(true);
        startScreen.SetActive(false);
        startButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        progressBar.gameObject.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        unpauseButton.gameObject.SetActive(false);
    }
}
