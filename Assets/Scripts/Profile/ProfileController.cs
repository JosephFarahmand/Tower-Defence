

public class ProfileController
{
    public Profile Profile { get; private set; }

    public delegate void OnChangeProperty(Profile profile);
    public OnChangeProperty onChangePropertyCallback;

    public ProfileController()
    {
        Profile = new Profile(500, 0);

        GameManager.Instance.Stats.OnChangeState += Stats_OnChangeState;
    }

    public void Initialization()
    {
        onChangePropertyCallback?.Invoke(Profile);
    }

    private void Stats_OnChangeState(GameStats.State currentState)
    {
        if (currentState == GameStats.State.Reset)
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        Profile = new Profile(500, 0);
        onChangePropertyCallback?.Invoke(Profile);
    }


    public void AddScore(int amount)
    {
        Profile.AddScore(amount);
        onChangePropertyCallback?.Invoke(Profile);
    }

    public void AddGold(int amount)
    {
        Profile.AddGold(amount);
        onChangePropertyCallback?.Invoke(Profile);
    }

    public bool HasEnoughGold(int itemPrice)
    {
        return Profile.GoldAmount >= itemPrice;
    }
}