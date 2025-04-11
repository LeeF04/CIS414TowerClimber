using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour, IObserver
{
    private void Start()
    {
        var subject = FindObjectOfType<GameEventSubject>();
        subject.AddObserver(this);
    }

    public void OnNotify(string eventType, object value)
    {
        if (eventType == "GameOver")
        {
            SceneManager.LoadScene(2); // Load Game Over scene
        }
    }

    private void OnDestroy()
    {
        var subject = FindObjectOfType<GameEventSubject>();
        subject.RemoveObserver(this);
    }
}
