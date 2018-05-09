using Assets.Scripts.Characters.Base;
using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjects;
using Assets.UI.Scripts;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    [RequireComponent(typeof(IController))]
    public class Player : Character
    {
        [Header("Settings")]
        public PlayerStats PlayerStats;

        void Awake()
        {
            Stats = PlayerStats;
            Controller = GetComponent<PlayerController>();
        }

        public override void IncreaseHealth(float health)
        {
            base.IncreaseHealth(health);

            PlayerGUI.instance.HealthBarValue = CurrentHealth;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            PlayerGUI.instance.HealthBarValue = CurrentHealth;
        }

        protected override void Die()
        {
            base.Die();

            LevelManager.instance.RestartCurrentLevel();
        }
    }
}