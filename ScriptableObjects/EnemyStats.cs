using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy Stats", menuName = "Judgement-Night/Enemies/Enemy Stats", order=2)]
    public class EnemyStats : BaseCharacterStats
    {
        [Header("State Transitions")]
        public float ChaseDistance;
    }
}