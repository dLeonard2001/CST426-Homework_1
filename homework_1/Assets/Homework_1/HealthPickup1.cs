using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class HealthPickup : Pickup
    {
        [Header("Parameters")] [Tooltip("Amount of health to heal on pickup")]
        public float HealAmount;
        public float ArmorAmount;
        public bool isArmor;

        protected override void OnPicked(PlayerCharacterController player)
        {
            Health playerHealth = player.GetComponent<Health>();
            
            // === added for homework_1 ===
            if (playerHealth && playerHealth.CanPickUpArmor() && isArmor)
            {
                playerHealth.AddArmor(ArmorAmount);
                PlayPickupFeedback();
                Destroy(gameObject);
            }
            // =============================
            else if (playerHealth && playerHealth.CanPickupHealth())
            {
                playerHealth.Heal(HealAmount);
                PlayPickupFeedback();
                Destroy(gameObject);
            }
        }
    }
}