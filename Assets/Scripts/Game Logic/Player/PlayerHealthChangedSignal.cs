namespace GameLogic
{
    public class PlayerHealthChangedSignal
    {
        public readonly int Health;
        public readonly int MaxHealth;

        public PlayerHealthChangedSignal(int health, int maxHealth)
        {
            Health = health;
            MaxHealth = maxHealth;
        }
    }
}