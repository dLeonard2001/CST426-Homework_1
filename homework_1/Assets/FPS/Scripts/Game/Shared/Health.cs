using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Unity.FPS.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("Maximum amount of health")] 
        public float MaxHealth = 10f;

        [Tooltip("Maximum armor for player")] 
        public float MaxArmor = 100f;
        public Image armorImage;
        public bool isEnemy;

        [Tooltip("Health ratio at which the critical health vignette starts appearing")]
        public float CriticalHealthRatio = 0.3f;

        public UnityAction<float, GameObject> OnDamaged;
        public UnityAction<float> OnHealed;
        public UnityAction OnDie;

        public float CurrentHealth { get; set; }
        public float CurrentArmor { get; set; }
        public bool Invincible { get; set; }
        public bool CanPickupHealth() => CurrentHealth < MaxHealth;
        public bool CanPickUpArmor() => CurrentArmor < MaxArmor;

        public float GetRatio() => CurrentHealth / MaxHealth;
        public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

        bool m_IsDead;

        void Start()
        {
            CurrentHealth = MaxHealth;
            CurrentArmor = MaxArmor;
        }

        // === added for homework_1 ===
        public void AddArmor(float armorAmount)
        {
            CurrentArmor += armorAmount;
            CurrentArmor = Mathf.Clamp(CurrentArmor, 0f, MaxArmor);
            armorImage.fillAmount = CurrentArmor / 10f * 0.1f;
        }
        // =============================

        public void Heal(float healAmount)
        {
            float healthBefore = CurrentHealth;
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            // call OnHeal action
            float trueHealAmount = CurrentHealth - healthBefore;
            if (trueHealAmount > 0f)
            {
                OnHealed?.Invoke(trueHealAmount);
            }
        }

        public void TakeDamage(float damage, GameObject damageSource)
        {
            if (Invincible)
                return;
            
            // === added for homework_1 ===
            if (CurrentArmor > 0 && !isEnemy)
            {
                if (CurrentArmor - damage < 0 && !isEnemy && CurrentArmor > 0)
                {
                    damage -= CurrentArmor;
                    CurrentArmor = 0;
                    CurrentHealth -= damage;
                    CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);
                    armorImage.fillAmount = (CurrentArmor) / 10f * 0.1f;
                }
                else
                {
                    CurrentArmor -= damage;
                    armorImage.fillAmount = (CurrentArmor) / 10f * 0.1f;
                }
                // =============================
            }
            else
            {
                float healthBefore = CurrentHealth;
                CurrentHealth -= damage;
                CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

                // call OnDamage action
                float trueDamageAmount = healthBefore - CurrentHealth;
                if (trueDamageAmount > 0f)
                {
                    OnDamaged?.Invoke(trueDamageAmount, damageSource);
                }
            }

            HandleDeath();
        }

        public void Kill()
        {
            CurrentHealth = 0f;

            // call OnDamage action
            OnDamaged?.Invoke(MaxHealth, null);

            HandleDeath();
        }

        void HandleDeath()
        {
            if (m_IsDead)
                return;

            // call OnDie action
            if (CurrentHealth <= 0f)
            {
                m_IsDead = true;
                OnDie?.Invoke();
            }
        }
    }
}