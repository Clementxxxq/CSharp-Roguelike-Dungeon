public class Experience
{
    private int currentXP = 0;
    private int currentLevel = 1;

    public int XPForNextLevel()
    {
        return 100 + (currentLevel - 1) * 50;
    }
    public int CurrentXP => currentXP;
    public int CurrentLevel => currentLevel;
    public bool GainXP(int amount)
    {
        currentXP += amount;
        bool leveledUp = false;

        while (currentXP >= XPForNextLevel())
        {
            currentXP -= XPForNextLevel();
            currentLevel++;
            leveledUp = true;
        }

        return leveledUp;
    }
    public void Reset()
    {
        currentXP = 0;
        currentLevel = 1;
    }
}