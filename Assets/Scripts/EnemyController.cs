using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 4f;
    private Vector2 moveInput;
    private GameObject target;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //target = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target == null)
        {
            rb.linearVelocity = Vector2.zero; // para o movimento
            return;
        }
        moveInput = (target.transform.position - this.transform.position).normalized;
        rb.linearVelocity = moveInput * speed;
    }

    public void SetTarget(GameObject tg)
    {
        target = tg;
    }
}
