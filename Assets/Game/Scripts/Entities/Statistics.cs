namespace Game
{
    public static class Statistics
    {
        public static event System.Action OnEnemyKill;
        public static event System.Action OnCardPickedUp;
        private static int EnemiesKilled { get; set; } = 0;
        private static int CardsPickedUp { get; set; } = 0;

        public static void EnemyKill()
        {
            EnemiesKilled++;
            OnEnemyKill?.Invoke();
        }
        public static void CardPickedUp()
        {
            CardsPickedUp++;
            OnCardPickedUp?.Invoke();
        }
    }
}