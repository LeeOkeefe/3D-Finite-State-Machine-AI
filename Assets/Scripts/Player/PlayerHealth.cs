using UnityEngine;

namespace Player
{
    internal sealed class PlayerHealth : MonoBehaviour
    {
        private HealthObject m_PlayerHealth;

        private void Start()
        {
            m_PlayerHealth = new HealthObject();
        }

        public void Kill()
        {
            m_PlayerHealth.DamageHealth(m_PlayerHealth.MaxHealth);
        }

        public void Damage(int damage)
        {
            if (m_PlayerHealth.IsDead)
            {
                Debug.Log("Player is dead");
            }

            m_PlayerHealth.DamageHealth(damage);
        }

        public void Heal(int health)
        {
            if (m_PlayerHealth.IsDead)
                return;

            m_PlayerHealth.Heal(health);
        }
    }
}
