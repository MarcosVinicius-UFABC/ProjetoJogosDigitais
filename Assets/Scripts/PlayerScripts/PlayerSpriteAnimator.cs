using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSpriteAnimator : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float treshold = .4f;

    private void Start()
    {
        rb = this.GetComponentInParent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 lv = rb.linearVelocity.magnitude > treshold ? rb.linearVelocity.normalized : Vector2.zero;
        if (Mathf.Abs(lv.y) < treshold && Mathf.Abs(lv.x) < treshold)
        {
            lv.y = 0;
            lv.x = 0;
        }
        else if (Mathf.Abs(lv.y) > Mathf.Abs(lv.x) - 0.2f)
        {
            lv.y = lv.y > 0 ? 1f : -1f;
            lv.x = 0f;
        }
        else
        {
            lv.y = 0f;
            lv.x = lv.x > 0 ? 1f : -1f;
        }

        if (lv.x < 0 && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
        else if (lv.x > 0 && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
        else
        {}

        animator.SetFloat("VelocityX", lv.x);
        animator.SetFloat("VelocityY", lv.y);

        animator.SetFloat("Velocity", Mathf.Abs(lv.x) + Mathf.Abs(lv.y));

        Debug.Log(Mathf.Abs(lv.x) + Mathf.Abs(lv.y));
    }
}
