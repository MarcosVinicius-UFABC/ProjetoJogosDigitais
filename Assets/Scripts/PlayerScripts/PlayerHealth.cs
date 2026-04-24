using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public override void IsDead()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
