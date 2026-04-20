using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public float xpToNextLevel = 100f;
    public float xpScaling = 1.5f;

    public int Level { get; private set; } = 1;
    public float CurrentXP { get; private set; } = 0f;

    public void AddXP(float amount)
    {
        CurrentXP += amount;
        if (CurrentXP >= xpToNextLevel)
            LevelUp();
    }

    void LevelUp()
    {
        CurrentXP -= xpToNextLevel;
        xpToNextLevel *= xpScaling;
        Level++;
        Debug.Log($"Level up! Now level {Level}");
    }
}
