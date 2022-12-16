using System;
using UnityEngine;

namespace Codebase.Components
{
    public class Health : MonoBehaviour
    {
        public event Action OnAddHealth;
        public event Action OnSubHealth;
        public event Action OnDead;
        
        [SerializeField]
        private int maxHealth;
        [SerializeField] 
        private int health;

        public int GetHealth()
        {
            return health;
        }
        
        public void AddHealth(int value)
        {
            health += value;
            health = Mathf.Clamp(health, 0, maxHealth);
            
            OnAddHealth?.Invoke();
        }

        public void SubHealth(int value)
        {
            health -= value;
            health = Mathf.Clamp(health, 0, maxHealth);
            
            OnSubHealth?.Invoke();

            if (health == 0)
            {
                OnDead?.Invoke();
            }
        }
    }
}