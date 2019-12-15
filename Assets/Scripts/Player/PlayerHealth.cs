using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    internal sealed class PlayerHealth : MonoBehaviour
    {
        private HealthObject m_PlayerHealth;
        private MouseCursor m_Cursor;

        private void Start()
        {
            m_PlayerHealth = new HealthObject();
            m_Cursor = GetComponent<MouseCursor>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Damage(25);
                Debug.Log(m_PlayerHealth.CurrentHealth);
            }
        }

        public void Kill()
        {
            Damage(m_PlayerHealth.MaxHealth);
        }

        public void Damage(int damage)
        {
            m_PlayerHealth.DamageHealth(damage);

            if (m_PlayerHealth.IsDead)
            {
                m_Cursor.ToggleMouse(true);
                PlayerPrefs.SetInt("LevelComplete", 0);
                PlayerPrefs.Save();
                SceneManager.LoadScene("EndLevel");
            }
        }

        public void Heal(int health)
        {
            if (m_PlayerHealth.IsDead)
                return;

            m_PlayerHealth.Heal(health);
        }
    }
}
