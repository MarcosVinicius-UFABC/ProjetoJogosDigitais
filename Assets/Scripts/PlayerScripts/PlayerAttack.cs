using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [System.Serializable] public class AttackData
    {
        public GameObject attackPrefab;
        public float attackCooldown;
        public float attackTimer = 0f;
        public int attackAmount = 1;
    }

    [System.Serializable] public class OrbitalData
    {
        public GameObject orbitalPrefab;
        public int orbCount = 3;
        public float orbitRadius = 5f;
        public float orbitSpeed = 180f;
        public float damage = 15f;
        public float damageInterval = 0.5f;
    }

    public List<AttackData> playerAttacks = new List<AttackData>();
    public List<OrbitalData> orbitalAttacks = new List<OrbitalData>();

    public GameObject projectilePrefab;
    public GameObject meleePrefab;
    public float fireRate = 0.3f;
    public float meleeOffset = 1f;
    [HideInInspector] public float damageMultiplier = 1f;

    void Update()
    {
        /*
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            FireProjectile();
            cooldown = fireRate;
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
            FireMelee();
        */
        AllAttacks();
    }

    void FireProjectile()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("PlayerAttack: projectilePrefab not assigned in Inspector.", this);
            return;
        }

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorld.z = 0f;
        Vector2 direction = (mouseWorld - transform.position).normalized;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        ProjectileAttack pa = proj.GetComponent<ProjectileAttack>();
        pa.damage *= damageMultiplier;
        pa.SetDirection(direction);
    }

    void FireMelee()
    {
        if (meleePrefab == null)
        {
            Debug.LogWarning("PlayerAttack: meleePrefab not assigned in Inspector.", this);
            return;
        }

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorld.z = 0f;
        Vector2 direction = (mouseWorld - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 spawnPos = transform.position + (Vector3)direction * meleeOffset;

        GameObject melee = Instantiate(meleePrefab, spawnPos, Quaternion.Euler(0f, 0f, angle), transform);
        melee.GetComponent<MeleeAttack>().damage *= damageMultiplier;
    }

    private void AllAttacks()
    {
        for (int i = 0; i < playerAttacks.Count; i++)
        {
            AttackData atk = playerAttacks[i];
            if (atk.attackPrefab == null || atk.attackCooldown < 0 || atk.attackAmount < 0)
            {
                Debug.LogWarning("PlayerAttack: invalid AttackData entry — skipping.");
                return;
            }
            if (atk.attackTimer <= 0)
            {
                for (int n = 0; n < atk.attackAmount; n++)
                {
                    Attack(atk.attackPrefab);
                }
                atk.attackTimer = atk.attackCooldown;
            }
            else
            {
                atk.attackTimer -= Time.deltaTime;
            }
        }
        
    }

    void Attack(GameObject attack)
    {
        if (attack == meleePrefab)
        {
            FireMelee();
        }
        else if (attack == projectilePrefab)
        {
            FireProjectile();
        }
        else
        {
            Debug.LogWarning("Attack not found!");
        }
    }

    public void ActivateOrbitals()
    {
        foreach (var data in orbitalAttacks)
            SpawnOrbitals(data);
    }

    void SpawnOrbitals(OrbitalData data)
    {
        if (data.orbitalPrefab == null)
        {
            Debug.LogWarning("OrbitalData: orbitalPrefab not assigned.", this);
            return;
        }

        float angleStep = 360f / data.orbCount;
        for (int i = 0; i < data.orbCount; i++)
        {
            GameObject orb = Instantiate(data.orbitalPrefab);
            OrbitalProjectile op = orb.GetComponent<OrbitalProjectile>();
            op.damage = data.damage;
            op.damageInterval = data.damageInterval;
            op.Init(transform, i * angleStep, data.orbitRadius, data.orbitSpeed);
        }
    }
}
