public class Profile
{
    public Profile(int goldAmount, int score)
    {
        GoldAmount = goldAmount;
        Score = score;
    }

    public int GoldAmount { get; private set; }
    public int Score { get; private set; }

    public void AddScore(int amount)
    {
        Score += amount;
    }

    public void AddGold(int amount)
    {
        GoldAmount += amount;
    }
}
