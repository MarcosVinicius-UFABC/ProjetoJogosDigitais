using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : Health
{
    public float xpDrop = 30f;
    public GameObject xpOrbPrefab;
    public int minOrbDrop = 1;
    public int maxOrbDrop = 5;

    public override void IsDead()
    {
        GameManager.Instance?.EnemyDied();
        if (xpOrbPrefab != null)
        {
            int count = Random.Range(minOrbDrop, maxOrbDrop + 1);
            float xpPerOrb = xpDrop / count;
            for (int i = 0; i < count; i++)
            {
                Vector2 scatter = Random.insideUnitCircle * 0.5f;
                Vector3 spawnPos = transform.position + new Vector3(scatter.x, scatter.y, 0f);
                GameObject orb = Instantiate(xpOrbPrefab, spawnPos, Quaternion.identity);
                orb.GetComponent<XPOrb>().xpValue = xpPerOrb;
            }
        }
        base.IsDead();
    }
}
