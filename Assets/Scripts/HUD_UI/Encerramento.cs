using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Encerramento : MonoBehaviour
{
    public void Update()
    {
        //Debug.Log("Updating");
        if(Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame  || Mouse.current.rightButton.wasPressedThisFrame)
        {
            QuitGame();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
