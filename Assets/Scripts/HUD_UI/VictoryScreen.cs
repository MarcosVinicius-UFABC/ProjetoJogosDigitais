using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public GameObject victoryScreen;

    public void Resume()
    {
        victoryScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        victoryScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NextStage()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.buildIndex+1);
        Resume();
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.name);
        Resume();
    }

    public void QuitToMenu()
    {
        Resume();
        UnityEngine.SceneManagement.SceneManager.LoadScene("0.MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
