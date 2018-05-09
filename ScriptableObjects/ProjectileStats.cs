using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Projectile Stats", menuName = "Judgement-Night/Projectiles/Projectile Stats", order = 0)]
    public class ProjectileStats : ScriptableObject
    {
        public float Damage = 15f;
        public float MaxSpeed = 20f;
        public float DestroyAfter = 4f;
    }
}