using UnityEngine;

public class BossController : EnemyController
{
    private GameObject tg;
    public float approachingDistance = 7f;
    private bool rushMode = false;
    private bool canRush = true;
    public float rushDuration = 1.5f;
    public float rushCooldown = 1.2f;
    private float rushTimer = 0f;
    private float cooldownTimer = 0f;
    private GameObject tempTarget;

    protected override void FixedUpdate()
    {
        if (tg == null)
            tg = GameObject.FindGameObjectWithTag("Player");

        if (tg == null) return;

        if (!rushMode && Vector3.Distance(tg.transform.position, transform.position) <= approachingDistance)
        {
            rushMode = true;
            speed *= 4;
        }

        if (rushMode)
            Rush(tg);
        else
            Move(tg);
    }

    void Rush(GameObject target)
    {
        if (canRush && cooldownTimer <= 0)
        {
            tempTarget = new GameObject("RushTarget");
            tempTarget.transform.position = target.transform.position;
            rushTimer = rushDuration;
            canRush = false;
        }

        if (cooldownTimer <= 0)
        {
            if (rushTimer > 0)
            {
                rushTimer -= Time.fixedDeltaTime;
                Move(tempTarget);
            }
            else
            {
                Destroy(tempTarget);
                tempTarget = null;
                rushTimer = rushDuration;
                cooldownTimer = rushCooldown;
            }
        }
        else
        {
            cooldownTimer -= Time.fixedDeltaTime;
            if (cooldownTimer <= 0)
                canRush = true;
        }
    }
}
