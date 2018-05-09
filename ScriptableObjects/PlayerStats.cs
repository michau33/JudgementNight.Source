using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu (menuName = "Judgement-Night/Player/Player Stats", fileName = "Player Stats", order = 0)]
    public class PlayerStats : BaseCharacterStats
    {
        [Header("Mouse and keyboard controls")]
        public float LookRotationSpeed;

        [Header ("GamepadButtons controls")]
        public float AnalogRotationSpeed;

    }
}