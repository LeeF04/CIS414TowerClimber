using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Written by Bryan Castaneda
public class Enemy_Patrol : MonoBehaviour
{
    public float speed;
    public float rayDist;
    private bool movingRight = true;
    public Transform groundDetect;

    private GameEventSubject eventSubject; // for observer pattern
    private IEnemyMediator mediator;       // for mediator pattern

    void Start()
    {
        eventSubject = FindObjectOfType<GameEventSubject>();

        // reference mediator
        EnemyMediator concreteMediator = FindObjectOfType<EnemyMediator>();
        if (concreteMediator != null)
        {
            mediator = concreteMediator;
            mediator.RegisterEnemy(this);
        }
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, rayDist);

        if (!groundCheck.collider)
        {
            FlipDirection();
        }

        if (mediator != null)
        {
            Vector3 center = mediator.GetGroupCenter();
            float dist = Vector3.Distance(transform.position, center);
            if (dist > 5f)
                speed = 3.5f;
            else
                speed = 2.0f;
        }
    }

    private void FlipDirection()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            eventSubject?.NotifyObservers("GameOver");
        }
    }
}
