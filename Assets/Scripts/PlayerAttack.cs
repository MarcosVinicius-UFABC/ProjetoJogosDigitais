using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            TestFunction();
        }
    }

    void TestFunction()
    {
    }

    void Attack()
    {
        Vector3 spawnPosition = transform.position + new Vector3(.86f, 0, 0);
        GameObject newAttack = Instantiate(attack, spawnPosition, Quaternion.identity);
    }
}
