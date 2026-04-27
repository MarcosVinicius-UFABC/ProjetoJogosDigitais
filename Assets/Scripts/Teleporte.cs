using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Teleporte : MonoBehaviour
{
    public TextMeshPro textMeshPro3D;
    private TextMeshPro inputPrompt;
    public Vector3 inputPromptOffset = new Vector3(-1, 0, 0);
    private bool playerNearby = false;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && inputPrompt == null)
        {
            playerNearby = true;
            Debug.Log("Entrou");
            inputPrompt = Instantiate(textMeshPro3D);
            inputPrompt.transform.SetParent(gameObject.transform);
            inputPrompt.transform.position = inputPromptOffset;
            inputPrompt.text = "E - Interagir";
            Debug.Log(inputPrompt.transform.position);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Debug.Log("Saiu");
            Destroy(inputPrompt);
            inputPrompt = null;
        }
    }

    void Update()
    {
        if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame)
        {
            SavePlayerData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
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
}
