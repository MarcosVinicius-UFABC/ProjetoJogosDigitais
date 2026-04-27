using System.Collections;
using UnityEngine;

public class TargetedCircleAttack : MonoBehaviour
{
    public float damage = 10f;
    public float radius = 2f;
    public float warningDuration = 0.4f;
    public float cooldownBetweenStrikes = 1f;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.sortingOrder = 10;
        transform.localScale = Vector3.zero;
    }

    void Start()
    {
        StartCoroutine(Execute());
    }

    IEnumerator Execute()
    {
        while (true)
        {
            EnemyController[] enemies = FindObjectsOfType<EnemyController>();
            if (enemies.Length == 0)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }

            Vector3 pos = enemies[Random.Range(0, enemies.Length)].transform.position;
            transform.position = new Vector3(pos.x, pos.y, -1f);
            transform.localScale = Vector3.zero;

            float elapsed = 0f;
            while (elapsed < warningDuration)
            {
                elapsed += Time.deltaTime;
                float scale = Mathf.Lerp(0f, radius * 2f, elapsed / warningDuration);
                transform.localScale = new Vector3(scale, scale, 1f);
                if (sr != null) sr.color = Color.Lerp(Color.yellow, Color.red, elapsed / warningDuration);
                yield return null;
            }

            transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);
            if (sr != null) sr.color = Color.red;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player")) continue;
                Health h = hit.GetComponent<Health>();
                if (h != null) h.TakeDamage(damage);
            }

            yield return new WaitForSeconds(0.15f);
            transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(cooldownBetweenStrikes);
        }
    }
}
