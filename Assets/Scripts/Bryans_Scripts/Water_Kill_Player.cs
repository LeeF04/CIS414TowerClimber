using UnityEngine;
using UnityEngine.SceneManagement; 

//Written by Bryan Castaneda
public class Water_Kill_Player : MonoBehaviour
{

    private GameEventSubject eventSubject; //for observer pattern


    void Start()
    {
        eventSubject = FindObjectOfType<GameEventSubject>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            eventSubject?.NotifyObservers("GameOver");
        }
    }
}
