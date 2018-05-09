using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class AutoParticlesDestroy : MonoBehaviour
    {
        ParticleSystem particleSystem;

        void Awake()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        void LateUpdate()
        {
            if (!particleSystem.isPlaying)
                Destroy(this.gameObject);
        }
    }
}