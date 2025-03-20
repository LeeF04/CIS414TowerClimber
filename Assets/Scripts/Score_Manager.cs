using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI jumpScoreText;
    private int jumpScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        ResetScore(); // Ensure the score is reset when the game starts
    }

    public void IncreaseScore()
    {
        jumpScore++;
        jumpScoreText.text = "PLATFORM SCORE\n" + jumpScore;

        PlayerPrefs.SetInt("FinalScore", jumpScore);
        PlayerPrefs.Save();
    }

    public int GetScore()
    {
        return jumpScore;
    }

    public void ResetScore()
    {
        jumpScore = 0;
        if (jumpScoreText != null)
        {
            jumpScoreText.text = "PLATFORM SCORE\n" + jumpScore;
        }
    }
}
