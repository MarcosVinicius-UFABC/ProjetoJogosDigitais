using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 0.3f;

    private float cooldown = 0f;

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            FireProjectile();
            cooldown = fireRate;
        }
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
}
