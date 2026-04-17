using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attack;
    private PlayerInputActions input; // nome do seu asset
    private Vector2 moveInput;

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
        Attack();
    }

    void Attack()
    {

        GameObject newAttack = Instantiate(attack, this.transform);
    }

    public Vector2 CheckDirection()
    {
        return moveInput;
    }
}
