using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public class BaseCharacterStats : ScriptableObject
    {
        [Header("Health")]
        public float Health;

        [Header("Movement")]
        public float MovementSpeed;

    }
}
