using System;

public class Experience
{
    private int currentXP = 0;
    private int currentLevel = 1;

    public int XPForNextLevel()
    {
        return 80 + (currentLevel - 1) * 35;
    }
    public int CurrentXP => currentXP;
    public int CurrentLevel => currentLevel;
    public bool GainXP(int amount)
    {
        int boostedAmount = (int)Math.Ceiling(amount * 1.25);
        currentXP += boostedAmount;
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
