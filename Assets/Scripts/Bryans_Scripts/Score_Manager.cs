using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour, ISubject
{
    public static ScoreManager instance;

    public TextMeshProUGUI jumpScoreText;
    private int jumpScore = 0;

    private List<IObserver> observers = new List<IObserver>();

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

        ResetScore(); // Ensure score is 0 at start
    }

    void Start()
    {
        if (jumpScoreText != null)
        {
            jumpScoreText.text = "PLATFORM SCORE\n" + jumpScore;
        }
    }

    public void IncreaseScore()
    {
        jumpScore++;
        jumpScoreText.text = "PLATFORM SCORE\n" + jumpScore;

        PlayerPrefs.SetInt("FinalScore", jumpScore);
        PlayerPrefs.Save();

        NotifyObservers("ScoreChanged", jumpScore);
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

    // Observer Pattern 
    public void AddObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }

    public void NotifyObservers(string eventType, object value = null)
    {
        foreach (var observer in observers)
        {
            observer.OnNotify(eventType, value);
        }
    }
}
