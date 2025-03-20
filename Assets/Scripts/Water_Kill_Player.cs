using UnityEngine;
using UnityEngine.SceneManagement; 

//Written by Bryan Castaneda
public class Water_Kill_Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2); // Load the Game Over scene
        }
      
    }
}
