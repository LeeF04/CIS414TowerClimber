// Created by Lee Fischer
// 4/6/25

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShadowController : MonoBehaviour
{
    [SerializeField] private float shadowSpeed = 5.0f;
    [SerializeField] private float actionDelay = 1.0f;
    private List<ICommand> commandQueue = new List<ICommand>();
    private Rigidbody2D shadowRb;

    [SerializeField] private ParticleSystem teleportPart;
    private ParticleSystem teleportPartInstance;

    // Methods
    private void Start()
    {
        shadowRb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        // Getting the necessary key presses
        if(Input.GetKeyDown(KeyCode.A))
        {
            ExecuteCommand(new ShadowLeft(this));
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            ExecuteCommand(new ShadowRight(this));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteCommand(new ShadowJump(this));
        }
    }
    public void Move(Vector3 aDirect)
    {
        transform.position = transform.position + aDirect * shadowSpeed;
    }
    private void ExecuteCommand(ICommand aCommand)
    {
        StartCoroutine(ExecuteDelay(aCommand, actionDelay));
        // Spawns the particles.
        SpawnTeleportParticles();
    }
    private IEnumerator ExecuteDelay(ICommand aCommand, float delay)
    {
        yield return new WaitForSeconds(delay);

        aCommand.Execute();
        commandQueue.Add(aCommand);
    }
    /*private IEnumerable ExecuteCommand(ICommand aCommand, float delay)
    {
        yield return new WaitForSeconds(delay);
        aCommand.Execute();
        commandQueue.Add(aCommand);
    }*/
    private void SpawnTeleportParticles()
    {
        teleportPartInstance = Instantiate(teleportPart, transform.position, Quaternion.identity);
    }
}
