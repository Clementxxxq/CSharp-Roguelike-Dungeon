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
        CurrentXP += amount;
        bool leveledUp = false;

        while (CurrentXP >= XPToNextLevel)
        {
            CurrentXP -= XPToNextLevel;
            Level++;
            XPToNextLevel = CalculateXPForNextLevel();
            leveledUp = true;
        }

        return leveledUp;
    }

    private void LevelUp()
    {
        currentLevel++;
        currentXP -= XPForNextLevel();
    }
    public void Reset()
    {
        currentXP = 0;
        currentLevel = 1;
    }
}