using System.Collections.Generic;
using UnityEngine;

public class LevelUpScreen : MonoBehaviour
{
    [System.Serializable] public class powerup
    {
        public GameObject attackPrefab;
        public int attackMaxLevel = 5;
        public int attackCurrentLevel = 1;
        public bool hasAttack = false;
    }

    public List<powerup> powerups;
}
