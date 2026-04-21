using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float damage = 25f;
    public float lifeTime = 0.2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;

        Health health = other.GetComponent<Health>();
        if (health != null)
            health.TakeDamage(damage);
    }
}
