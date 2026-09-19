using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private string questions;
    private int correctAnswer;
    private int wrongAnswer;
    public int randomSide;
    private float moveSpeed;

    [SerializeField] private TMP_Text questions_label;
    [SerializeField] private TMP_Text leftAnswer;
    [SerializeField] private TMP_Text rightAnswer;
    [SerializeField] private GameManager gameManager;

    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void OnEnable()
    {
        float distance = Mathf.Abs(gameManager.spawnManager.transform.position.z - gameManager.player.transform.position.z);
        moveSpeed = distance / gameManager.levelDuration;
    }

    void Update()
    {
        if (CompareTag("AnsSignboard"))
        {
            //Moving the answer signboard towards the player
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
            //Destroy out of bound objects
            if (transform.position.z > 80)
            {
                Destroy(gameObject);
            }          
        }
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
        //Using diff math operators for different difficulties
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
        //Provide value for diff ans
        do
        {
            wrongAnswer = correctAnswer + Random.Range(-5, 6);
        }
        while (wrongAnswer == correctAnswer);
    }

    public void DisplayAnswers(DialogueManager answerParameter)
    {
        questions_label.gameObject.SetActive(true);
        questions_label.text = questions.ToString();

        answerParameter.leftAnswer.gameObject.SetActive(true);
        answerParameter.rightAnswer.gameObject.SetActive(true);

        randomSide = Random.Range(0, 2);
        if (randomSide == 0)
        {
            answerParameter.leftAnswer.text = correctAnswer.ToString();
            answerParameter.rightAnswer.text = wrongAnswer.ToString();
        }
        else
        {
            answerParameter.leftAnswer.text = wrongAnswer.ToString();
            answerParameter.rightAnswer.text = correctAnswer.ToString();
        }
    }
}
