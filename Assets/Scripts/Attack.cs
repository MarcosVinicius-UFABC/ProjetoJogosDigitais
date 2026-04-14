using UnityEngine;

public class Attack : MonoBehaviour
{
    public float lifeTime = 2f;
    public float damage = 5f;
    public Vector3 attackSize = new Vector3(1, 1, 0);

    void Start()
    {
        transform.localScale = attackSize;
    }

    void FixedUpdate()
    {
        if (lifeTime >= 0)
        {
            lifeTime -= Time.fixedDeltaTime;
            print(lifeTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
