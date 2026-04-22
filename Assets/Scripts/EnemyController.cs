using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 4f;
    public float damage = 30f;
    /*public float xpDrop = 10f;
    public GameObject xpOrbPrefab;*/
    protected Vector2 moveInput;
    protected GameObject target;
    protected Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //target = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(target);
    }

    public void SetTarget(GameObject tg)
    {
        target = tg;
    }

    public void Move(GameObject target)
    {
        if(target == null)
        {
            rb.linearVelocity = Vector2.zero; // para o movimento
            return;
        }
        moveInput = (target.transform.position - this.transform.position).normalized;
        rb.linearVelocity = moveInput * speed;
    }

    /*void OnDestroy()
    {
        GameManager.Instance?.EnemyDied();
        if (xpOrbPrefab != null)
        {
            GameObject orb = Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
            orb.GetComponent<XPOrb>().xpValue = xpDrop;
        }
    }*/

    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage * Time.deltaTime);
        }
    }
}
