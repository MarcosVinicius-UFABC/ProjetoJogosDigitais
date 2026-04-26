using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    void Start()
    {
        if (PlayerPersistentData.hasData)
        {
            SetMax(PlayerPersistentData.maxHealth);
            SetCurrent(PlayerPersistentData.currentHealth);
        }
        else
        {
            SetCurrent(maxHealth);
        }
    }

    public override void IsDead()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
