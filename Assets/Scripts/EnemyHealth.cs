using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : Health
{
    public float xpDrop = 10f;
    public GameObject xpOrbPrefab;
    protected override void IsDead()
    {
        GameManager.Instance?.EnemyDied();
        if (xpOrbPrefab != null)
        {
            GameObject orb = Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
            orb.GetComponent<XPOrb>().xpValue = xpDrop;
        }
        base.IsDead();
    }
}
