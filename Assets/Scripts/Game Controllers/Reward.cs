public static class Reward
{
    public static void KillEnemy(Enemy enemy)
    {
        var controller = GameManager.Instance.ProfileController;
        int goldReward = 0;
        int scoreReward = 0;
        switch (enemy.Type)
        {
            case CharacterType.Spearman:
                scoreReward = 10;
                goldReward = 5;
                break;
            case CharacterType.Archer:
                scoreReward = 15;
                goldReward = 10;
                break;
            case CharacterType.Swordman:
                scoreReward = 20;
                goldReward = 15;
                break;
            case CharacterType.Mage:
                scoreReward = 30;
                goldReward = 25;
                break;
        }
        controller.AddGold(goldReward);
        controller.AddScore(scoreReward);
    }
}