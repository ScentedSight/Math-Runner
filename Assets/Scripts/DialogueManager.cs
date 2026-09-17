using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private string questions;
    private int correctAnswer;
    private int wrongAnswer;
    public int randomSide;
    [SerializeField] private TMP_Text questions_label;
    [SerializeField] private TMP_Text leftAnswer;
    [SerializeField] private TMP_Text rightAnswer;
    [SerializeField] private GameManager gameManager;

    public void GenerateQuestion()
    {
        int number1 = Random.Range(1, 11);
        int number2 = Random.Range(1, 11);

        // 0 = +
        // 1 = -
        // 2 = ×
        // 3 = ÷
        int operatorIndex;

        if (gameManager.currentLevel <= 10)
        {
            operatorIndex = Random.Range(0, 2);
        }
        else if (gameManager.currentLevel <= 20)
        {
            operatorIndex = Random.Range(0, 3);
        }
        else
        {
            operatorIndex = Random.Range(0, 4);
        }

        switch (operatorIndex)
        {
            case 0:
                correctAnswer = number1 + number2;
                questions = number1 + " + " + number2 + "?";
                break;

            case 1:
                correctAnswer = number1 - number2;
                questions = number1 + " - " + number2 + "?";
                break;

            case 2:
                correctAnswer = number1 * number2;
                questions = number1 + " × " + number2 + "?";
                break;

            case 3:
                // Generate division that always produces a whole-number answer.
                correctAnswer = number1;

                int divisor = number2;
                int dividend = correctAnswer * divisor;

                questions = dividend + " ÷ " + divisor + "?";
                break;
        }

        do
        {
            wrongAnswer = correctAnswer + Random.Range(-5, 6);
        }
        while (wrongAnswer == correctAnswer);
    }

    public void DisplayAnswers()
    {
        questions_label.gameObject.SetActive(true);
        leftAnswer.gameObject.SetActive(true);
        rightAnswer.gameObject.SetActive(true);

        questions_label.text = questions.ToString();
        randomSide = Random.Range(0, 2);

        if (randomSide == 0)
        {
            leftAnswer.text = correctAnswer.ToString();
            rightAnswer.text = wrongAnswer.ToString();
        }
        else
        {
            leftAnswer.text = wrongAnswer.ToString();
            rightAnswer.text = correctAnswer.ToString();
        }
    }
}
