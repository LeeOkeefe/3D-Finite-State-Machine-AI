using System;

namespace Player
{
    internal sealed class HealthObject
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; }
        public bool IsDead => CurrentHealth <= 0;

        public HealthObject(int maxHealth)
        {
            if (maxHealth <= 0)
                throw new ArgumentException($"{maxHealth} was not a positive value.");

            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }

        /// <summary>
        /// Sets the max health to a default of 100
        /// </summary>
        public HealthObject()
        {
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
        }

        public void DamageHealth(int damage)
        {
            if (IsDead)
                return;

            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
            }
        }

        public void Heal(int health)
        {
            if (IsDead)
                return;

            CurrentHealth += health;

            if (CurrentHealth >= MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
    }
}
