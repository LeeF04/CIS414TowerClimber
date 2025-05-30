using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Bryan Castaneda's code, editted by Michael Anglemier
public class Axe_Patrol : MonoBehaviour
{
    public float speed;
    public float rayDist;
    private bool movingRight;
    public Transform groundDetect;

    private GameEventSubject eventSubject; //for observer pattern

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        /*
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, rayDist);

        if (groundCheck.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        */

    }

    void Start()
    {
        eventSubject = FindObjectOfType<GameEventSubject>();

        int randomDirection = Random.Range(0, 2);

        if (randomDirection == 0)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            eventSubject?.NotifyObservers("GameOver");
        }
    }

}