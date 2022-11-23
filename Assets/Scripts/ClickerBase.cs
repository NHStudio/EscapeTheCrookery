public static class tmp { // Заглушка
    // public static int Money = 10000;
    public static int PlayerBaseHealth = 3;
    public static int MedicBaseHeal = 3;
    public static int BaseDamage = 1;
    public static float Speed = 1;
    public static float FireRate = 1;
    public static float Luck = 0.1f;
    public static int DropAmount = 1;
}

public interface ClickerBase {
    public void Action();

    public int GetCurrentLevel();

    public int GetMaxLevel();
}
