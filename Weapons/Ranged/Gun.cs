using UnityEngine;
using System.Collections;
using Assets.Scripts.Cameras;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Projectiles;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Weapons.Events;
using Assets.UI.Scripts;

namespace Assets.Scripts.Weapons.Ranged
{
    public class Gun : Weapon
    {
        public GunStats GunStats;
        public Transform FirePoint;

        float timeBetweenShots;

        int currentBulletsAmount;
        public int CurrentBulletsAmount
        {
            get { return currentBulletsAmount; }
            private set
            {
                currentBulletsAmount = value;
                OnAmmoAmountChanged(new AmmoChangedEventArgs(currentBulletsAmount));
            }
        }

        public bool IsReloading { get; private set; }

        void OnEnabled()
        {
            PlayerGUI.instance.CurrentWeaponBulletsAmount = CurrentBulletsAmount;
        }

        void Start()
        {
            CurrentBulletsAmount = GunStats.MagazineSize;
            timeBetweenShots = GunStats.FireRate;
            IsReloading = false;
        }

        public override void Use()
        {
            if (IsReloading)
                return;

            timeBetweenShots += Time.deltaTime;

            if (CurrentBulletsAmount > 0 && timeBetweenShots >= GunStats.FireRate) 
            {
                var bullet = Instantiate(GunStats.BulletPrefab, FirePoint.position, FirePoint.rotation).GetComponent(typeof(Projectile));
                var mouseWorldPos = CameraController.instance.GetCursorWorldPosition(FirePoint.position);

                ((Projectile) bullet)
                    .LaunchProjectile (FirePoint, (mouseWorldPos - FirePoint.position).normalized, GunStats.FireForce);
     
                CurrentBulletsAmount--; // event is being handled on property's set
                PlayerGUI.instance.CurrentWeaponBulletsAmount = CurrentBulletsAmount;   // show current bullets amount in UI

                GameObject muzzleFlash = GunStats.MuzzleFlash;
                if (muzzleFlash != null)
                    Instantiate(muzzleFlash, FirePoint);

                AudioClip shootSound = GunStats.shootSound;
                if (shootSound != null)
                    AudioSource.PlayOneShot(shootSound);

                timeBetweenShots = 0f;
            }
        }

        /// <summary>
        /// ReloadCoroutine current range weapon whether you are not already reloading or magazine is full
        /// </summary>
        public override void ReloadWeapon () {
            if (IsReloading || CurrentBulletsAmount == GunStats.MagazineSize)
                return;

            StartCoroutine (ReloadCoroutine ());
        }

        /// <summary>
        /// Interrupt the reloading process only when you reloading
        /// </summary>
        public override void InterruptReloading () {
            if (!IsReloading)
                return;

            IsReloading = false;
        }

        /// <summary>
        /// Realoding coroutine. Uses ReloadTime from weapon stats.
        /// </summary>
        /// <returns></returns>
        IEnumerator ReloadCoroutine () {
            IsReloading = true;

            while (CurrentBulletsAmount <= GunStats.MagazineSize && IsReloading)
            {
                CurrentBulletsAmount++;

                PlayerGUI.instance.CurrentWeaponBulletsAmount = CurrentBulletsAmount;

                yield return new WaitForSeconds (GunStats.ReloadTime / GunStats.MagazineSize);
            }

            IsReloading = false;
        }
    }
}