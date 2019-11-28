namespace Player
{
    internal sealed class PlayerStats
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; }

        public PlayerStats()
        {
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
        }

        public void DamageHealth(int damage)
        {
            CurrentHealth -= damage;
        }

        public void Heal(int health)
        {
            CurrentHealth += health;
        }
    }
}
