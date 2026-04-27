public static class PlayerPersistentData
{
    public static bool hasData = false;

    // XP & Level
    public static int level = 1;
    public static float currentXP = 0f;
    public static float xpToNextLevel = 100f;

    // Health
    public static float maxHealth;
    public static float currentHealth;

    // Attacks
    public static float damageMultiplier = 1f;
    public static bool projectileUnlocked = false;
    public static bool orbitalsUnlocked = false;
    public static bool targetedCircleUnlocked = false;
}
