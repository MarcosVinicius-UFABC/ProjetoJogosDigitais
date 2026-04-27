using UnityEngine;

public class BossController : EnemyController
{
    private GameObject tg;
    public float speedMultiplier = 5f;
    public float approachingDistance = 8f;
    private bool readyToRush = false;
    public float preparingCooldown = .5f;
    private float preparingTimer = 0f;
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
        {
            tg = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            if (Vector3.Distance(tg.transform.position, this.transform.position) <= approachingDistance && !rushMode)
            {
                rushMode = true;
                speed *= speedMultiplier;
                Debug.Log("Once");
            }
            else
            {
                Debug.Log("Begin");
                Movement(tg);
            }
        }
    }
    void Movement(GameObject target)
    {
        if (rushMode)
        {
            //Prepara -> Investida -> Descanso -> Recomeça
            Rush(target);
        }
        else
            return;//Move(tg);
    }

    void Rush(GameObject target)
    {
        if (canRush && cooldownTimer <= 0)
        {
            tempTarget = new GameObject("RushTarget");
            tempTarget.transform.SetParent(gameObject.transform);
            tempTarget.transform.position = target.transform.position;
            rushTimer = rushDuration;
            canRush = false;
        }

        if (cooldownTimer <= 0)
        {    
            if (!readyToRush)
            {
                preparingTimer += Time.fixedDeltaTime;
                if (preparingTimer >= preparingCooldown)
                {
                    readyToRush = true;
                }
            }
            else if (rushTimer > 0)
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
            readyToRush = false;
            canRush = true;
            Debug.Log("Ready");
        }
    }
}
