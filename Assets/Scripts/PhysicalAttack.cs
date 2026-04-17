using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicalAttack : Attack
{
    void FixedUpdate()
    {
        base.SetPositionOnParent();
        base.Die();
    }
}
