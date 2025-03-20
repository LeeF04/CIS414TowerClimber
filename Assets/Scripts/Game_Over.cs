using UnityEngine;
using TMPro;

//Written by Bryan Castaneda

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        finalScoreText.text = "FINAL SCORE\n" + finalScore;
    }
}
