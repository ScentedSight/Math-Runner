using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public string questions;
    private int correctAnswer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Awake()
    {
        
    }

    void OnEnable()
    {
        
    }

    private void DialogueStart()
    {
        
    }

    public void GenerateQuestion()
    {
        int number1 = Random.Range(1, 11);
        int number2 = Random.Range(1, 11);

        // 0 = +
        // 1 = -
        // 2 = ×
        // 3 = ÷
        int operatorIndex;

        if (GameManager.currentLevel <= 10)
        {
            operatorIndex = Random.Range(0, 1);
        }
        else if (GameManager.currentLevel <= 20)
        {
            operatorIndex = Random.Range(0, 2);
        }
        else
        {
            operatorIndex = Random.Range(0, 3);
        }

        switch (operatorIndex)
        {
            case 0:
                correctAnswer = number1 + number2;
                questions = number1 + " + " + number2;
                break;

            case 1:
                correctAnswer = number1 - number2;
                questions = number1 + " - " + number2;
                break;

            case 2:
                correctAnswer = number1 * number2;
                questions = number1 + " × " + number2;
                break;

            case 3:
                // Generate division that always produces a whole-number answer.
                correctAnswer = number1;

                int divisor = number2;
                int dividend = correctAnswer * divisor;

                questions = dividend + " ÷ " + divisor;
                break;
        }
    }

    public string GetQuestion()
    {
        return questions;
    }

    public int GetCorrectAnswer()
    {
        return correctAnswer;
    }

    public void SetLevel(int level)
    {
        GameManager.currentLevel = level;
    }
}
