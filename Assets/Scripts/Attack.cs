using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float lifeTime = 2f;
    public float damage = 5f;
    public Vector3 attackSize = new Vector3(1, 1, 0);
    
    private Rigidbody2D rb;
    private Vector3 moveDirection;
    private Vector3 lastMoveDirection = Vector3.right;
    private float angle = 0f;
    public Vector3 offset = new Vector3(1f, 1.5f, 0);

    private Collider2D[] inimigos;

    void Start()
    {
        rb = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        transform.localScale = attackSize;
        SetPositionOnParent();
    }

    void FixedUpdate()
    {
        CheckCollisions();
        Die();
    }

    protected void SetPositionOnParent()
    {
        moveDirection = rb.linearVelocity;
        if(moveDirection != Vector3.zero)
        {
            if(moveDirection.x > 0f)
            {
                lastMoveDirection.x = 1f;
            }
            else if(moveDirection.x < 0f)
            {
                lastMoveDirection.x = -1f;
            }
            else
            {
                lastMoveDirection.x = 0f;
            }

            if(moveDirection.y > 0f)
            {
                lastMoveDirection.y = 1f;
            }
            else if(moveDirection.y < 0f)
            {
                lastMoveDirection.y = -1f;
            }
            else
            {
                lastMoveDirection.y = 0f;
            }

            if (lastMoveDirection.x != 0 && lastMoveDirection.y != 0)
            {
                lastMoveDirection *= Mathf.Sqrt(2)/2;
            }
        }

        angle = Mathf.Atan2(lastMoveDirection.y,lastMoveDirection.x) * Mathf.Rad2Deg;

        transform.localPosition = Vector3.Scale(offset, lastMoveDirection);
        transform.localRotation = Quaternion.Euler (0, 0, angle);
    }

    void CheckCollisions()
    {
        inimigos = Physics2D.OverlapBoxAll(transform.position, transform.localScale, angle);
        print(inimigos);
    }

    void OrEnter2D(Collider2D collision)
    {
        print(collision);
    }

    protected void Die()
    {
        if (lifeTime >= 0)
        {
            lifeTime -= Time.fixedDeltaTime;
            //print(lifeTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
