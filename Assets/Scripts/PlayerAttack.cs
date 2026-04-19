using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject meleePrefab;
    public float fireRate = 0.3f;
    public float meleeOffset = 1f;

    private float cooldown = 0f;

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            FireProjectile();
            cooldown = fireRate;
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
            FireMelee();
    }

    void FireProjectile()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("PlayerAttack: projectilePrefab not assigned in Inspector.", this);
            return;
        }

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorld.z = 0f;
        Vector2 direction = (mouseWorld - transform.position).normalized;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<ProjectileAttack>().SetDirection(direction);
    }

    void FireMelee()
    {
        if (meleePrefab == null)
        {
            Debug.LogWarning("PlayerAttack: meleePrefab not assigned in Inspector.", this);
            return;
        }

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorld.z = 0f;
        Vector2 direction = (mouseWorld - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 spawnPos = transform.position + (Vector3)direction * meleeOffset;

        Instantiate(meleePrefab, spawnPos, Quaternion.Euler(0f, 0f, angle), transform);
    }
}
