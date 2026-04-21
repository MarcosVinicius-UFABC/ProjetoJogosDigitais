using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public float xpValue = 10f;
    public float pickupRange = 3f;
    public float moveSpeed = 6f;

    private Transform player;

    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= pickupRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        other.GetComponent<PlayerXP>()?.AddXP(xpValue);
        Destroy(gameObject);
    }
}
