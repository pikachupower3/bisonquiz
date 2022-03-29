using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class QuizManager : MonoBehaviour
{
    public List<QandA> QnA;
    public List<QandT> QnT;
    public TextMeshProUGUI[] textFields;
    public GameObject[] options;
    public GameObject Buttons;
    public GameObject Fields;
    public TMP_InputField[] inputFields;
    public bool FieldsEnabled = false;
    public bool ButtonsEnabled = false;
    public int currentQuestion;
    public string Answer1;
    public string Answer2;

    public TextMeshProUGUI QuestionTxt;

    public void ReadField1(string answer1)
    {
        Answer1 = answer1;
    }

    public void ReadField2(string answer2)
    {
        Answer2 = answer2;
    }
    
    public int CheckAnswers()
    {
        int correcta = 0;
        for (int i = 0; i < QnT[currentQuestion].Separator; i++)
        {
            if (Answer1 == QnT[currentQuestion].CorrectAnswers[i])
            {
                correcta++;
            }
        }
        for (int j = QnT[currentQuestion].Separator; j < QnT[currentQuestion].CorrectAnswers.Length; j++)
        {
            if (Answer2 == QnT[currentQuestion].CorrectAnswers[j])
            {
                correcta++;
            }
        }
        return correcta;
    }

    private void Start()
    {
        StoreScore.Score = 0;
        Buttons.SetActive(false);
        Fields.SetActive(false);
        generateQuestion();
    }

    public void nextQuestion(bool correct)
    {
        if (correct)
        {
            StoreScore.Score++;
        }
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void nextQuestion1()
    {
        if (CheckAnswers() == 2)
        {
            StoreScore.Score++;
        }
        QnT.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            int button = UnityEngine.Random.Range(0, QnA[currentQuestion].Answers.Length);
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = QnA[currentQuestion].Answers[button];
            RemoveAt(ref QnA[currentQuestion].Answers, button);
            //QnA[currentQuestion].Answers.RemoveAt(button);

            if (QnA[currentQuestion].CorrectAnswer == button + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
        // finally, let's decrement Array's size by one
        Array.Resize(ref arr, arr.Length - 1);
    }

    void SetText()
    {
        if (textFields.Length > QnT[currentQuestion].Text.Length)
        {
            textFields[textFields.Length - 1].text = "";
            Debug.Log("Yes");
        }
        Debug.Log(textFields.Length);
        Debug.Log(QnT[currentQuestion].Text.Length);
        for (int i = 0; i < QnT[currentQuestion].Text.Length; i++)
        {
            textFields[i].text = QnT[currentQuestion].Text[i];
        }
        for (int j = 0; j < inputFields.Length; j++)
        {
            inputFields[j].text = "";
        }
    }

    void generateQuestion()
    {
        if (QnA.Count == 0 && QnT.Count == 0)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            int type = chooseType();
            if (type == 1)
            {
                currentQuestion = UnityEngine.Random.Range(0, QnA.Count);
                QuestionTxt.text = QnA[currentQuestion].Question;
                if (!ButtonsEnabled)
                {
                    Buttons.SetActive(true);
                    ButtonsEnabled = true;
                    Fields.SetActive(false);
                    FieldsEnabled = false;
                }
                SetAnswer();
            }
            else if (type == 0)
            {
                currentQuestion = UnityEngine.Random.Range(0, QnT.Count);
                QuestionTxt.text = "Fill in the missing words";
                if (!FieldsEnabled)
                {
                    Buttons.SetActive(false);
                    ButtonsEnabled = false;
                    Fields.SetActive(true);
                    FieldsEnabled = true;
                }
                SetText();
            }
        }
    }

    public int chooseType()
    {
        if (QnT.Count == 0)
        {
            return 1;
        }
        else if (QnA.Count == 0)
        {
            return 0;
        }
        else
        {
            return UnityEngine.Random.Range(0, 2);
        }
    }
}