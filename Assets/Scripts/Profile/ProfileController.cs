using UnityEngine;

public class ProfileController : MonoBehaviour
{
    public Profile Profile { get; private set; }

    public delegate void OnChangeProperty(Profile profile);
    public OnChangeProperty onChangePropertyCallback;

    public void Initialization()
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