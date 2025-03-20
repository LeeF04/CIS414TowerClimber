using UnityEngine.SceneManagement;
using UnityEngine;

//Written by Bryan Castaneda
public class Scene_Manager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}