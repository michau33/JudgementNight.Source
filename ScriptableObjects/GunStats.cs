using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu (fileName = "Gun Stats", menuName = "Judgement-Night/Gun Stats", order = 0)]
    public class GunStats : ScriptableObject
    {
        public GameObject BulletPrefab = null;
        public GameObject MuzzleFlash = null;

        public float FireForce = 1500f;
        public float FireRate = 0.1f;

        public float ReloadTime = 2.5f;
        public int MagazineSize = 15;

        public AudioClip shootSound;
    }
}