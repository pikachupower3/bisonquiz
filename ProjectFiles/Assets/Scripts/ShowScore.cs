using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShowScore : MonoBehaviour
{
    public List<QandA> QnA;
    public TMPro.TextMeshProUGUI ScoreTxt;
    public void Start()
    {
        ScoreTxt.text = "Your Score: " + StoreScore.Score + " out of 12!";
    }

    public void StartQuiz()
    {
        SceneManager.LoadScene(1);
    }
}
