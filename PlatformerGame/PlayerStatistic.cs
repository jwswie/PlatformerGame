namespace PlatformerGame
{
    public class PlayerStatistic
    {
        public PlayerStatistic(int score, int collectedCoins)
        {
            Score = score;
            CollectedCoins = collectedCoins;
        }

        public int Score { get; set; }
        public int CollectedCoins { get; set; }
    }
}
