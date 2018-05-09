using Assets.Scripts.Characters.Base;
using Assets.Scripts.Interfaces;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;


namespace Assets.Scripts.Characters
{
    [RequireComponent(typeof(IController))]
    public class Enemy : Character, ISpawnable
    {
        [Header("Settings")]
        public EnemyStats EnemyStats;

        public bool IsAwake { get; set; }

        void Awake()
        {
            Stats = EnemyStats;
            Controller = GetComponent<EnemyController>();
        }

        public override void IncreaseHealth(float health)
        {
            base.IncreaseHealth(health);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }

        protected override void Die()
        {
            base.Die();
        }

        public void OnObjectSpawned()
        {
            Debug.Log("I'm spawned " + this.gameObject.name);
        }
    }
}