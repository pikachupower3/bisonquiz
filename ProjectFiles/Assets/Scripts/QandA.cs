[System.Serializable]
public class QandA
{
    public string Question;
    public string[] Answers;
    public int CorrectAnswer;
}

[System.Serializable]
public class QandT
{
    public string[] Text;
    public string[] CorrectAnswers;
    public int Separator;
}