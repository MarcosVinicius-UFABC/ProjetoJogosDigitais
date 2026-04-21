using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public float CurrentHealth => currentHealth;
    
    public void SetMax(float v)
    {
        maxHealth = v;
        if (currentHealth > maxHealth)
        {
            SetCurrent(maxHealth);
        }
    }
    public void SetCurrent(float v)
    {
        currentHealth = v;
        if (currentHealth < 0)
        {
            IsDead();
        }
    }
    public void AddMax(float v)
    {
        maxHealth += v;
        if (currentHealth > maxHealth)
        {
            SetCurrent(maxHealth);
        }
    }
    public void ReceiveHealing(float v)
    {
        currentHealth += Mathf.Abs(v);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void TakeDamage(float v)
    {
        currentHealth -= Mathf.Abs(v);
        if (currentHealth < 0)
        {
            IsDead();
        }
    }
    public void MultiplyMax(float v)
    {
        maxHealth *= v;
        if (currentHealth > maxHealth)
        {
            MultiplyCurrent(v);
        }
    }
    public void MultiplyCurrent(float v)
    {
        currentHealth *= v;
        if (currentHealth < 0)
        {
            IsDead();
        }
    }

    protected virtual void IsDead()
    {
        Destroy(gameObject);
        print("Destroyed object");
    }



    void Start()
    {
        currentHealth = maxHealth;
        print(currentHealth);
    }
}
