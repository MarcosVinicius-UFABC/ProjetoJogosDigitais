using UnityEngine;
using System.Collections.Generic;

public class OrbitalProjectile : MonoBehaviour
{
    public float damage = 15f;
    public float damageInterval = 0.5f;

    private Transform player;
    private float angle;
    private float orbitRadius;
    private float orbitSpeed;
    private Dictionary<Collider2D, float> hitCooldowns = new Dictionary<Collider2D, float>();

    public void Init(Transform playerTransform, float startAngle, float radius, float speed)
    {
        player = playerTransform;
        angle = startAngle;
        orbitRadius = radius;
        orbitSpeed = speed;
    }

    void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }

        angle += orbitSpeed * Time.deltaTime;

        float rad = angle * Mathf.Deg2Rad;
        transform.position = player.position + new Vector3(Mathf.Cos(rad) * orbitRadius, Mathf.Sin(rad) * orbitRadius, 0f);

        var keys = new List<Collider2D>(hitCooldowns.Keys);
        foreach (var key in keys)
        {
            hitCooldowns[key] -= Time.deltaTime;
            if (hitCooldowns[key] <= 0f)
                hitCooldowns.Remove(key);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;
        if (hitCooldowns.ContainsKey(other)) return;

        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            hitCooldowns[other] = damageInterval;
        }
    }
}
