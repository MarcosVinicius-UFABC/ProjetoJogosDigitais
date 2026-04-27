using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float damage = 25f;
    public float lifeTime = 0.2f;
    private AudioSource attackSoundEffect;

    void Start()
    {
        Destroy(gameObject, lifeTime);

        GameObject audio = GameObject.Find("AudioAttack");
        if (audio != null)
            attackSoundEffect = audio.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;

        Health health = other.GetComponent<Health>();
        if (health != null) {
            health.TakeDamage(damage);
            attackSoundEffect.Play();
        }
    }
}
