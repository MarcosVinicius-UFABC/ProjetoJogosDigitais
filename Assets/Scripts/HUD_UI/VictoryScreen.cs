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
        SavePlayerData();
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.buildIndex+1);
        Resume();
    }

    void SavePlayerData()
    {
        PlayerXP xp = FindFirstObjectByType<PlayerXP>();
        PlayerHealth health = FindFirstObjectByType<PlayerHealth>();
        PlayerAttack attack = FindFirstObjectByType<PlayerAttack>();

        if (xp == null || health == null || attack == null) return;

        PlayerPersistentData.hasData = true;
        PlayerPersistentData.level = xp.Level;
        PlayerPersistentData.currentXP = xp.CurrentXP;
        PlayerPersistentData.xpToNextLevel = xp.xpToNextLevel;
        PlayerPersistentData.maxHealth = health.maxHealth;
        PlayerPersistentData.currentHealth = health.CurrentHealth;
        PlayerPersistentData.damageMultiplier = attack.damageMultiplier;
        PlayerPersistentData.projectileUnlocked = attack.ProjectileActive;
        PlayerPersistentData.orbitalsUnlocked = attack.OrbitalsActive;
        PlayerPersistentData.targetedCircleUnlocked = attack.TargetedCircleActive;
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
