using System;
using Assets.Scripts.Weapons.Events;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class Weapon : MonoBehaviour 
    {
        public WeaponType WeaponType;

        protected Animator Animator;
        protected AudioSource AudioSource;

        public bool IsAiming { get; set; }

        public event EventHandler<WeaponEnabledEventArgs> WeaponEnabled;
        public event EventHandler<AmmoChangedEventArgs> AmmoAmountChanged;
        public event EventHandler WeaponDiscarded;

        void Awake()
        {
            Animator = GetComponentInChildren<Animator>();
            AudioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// This is helpful when you changing weapon and you want to inform i.args. UI to change label depending on weapon type.
        /// </summary>
        void OnEnable()
        {
            OnWeaponEnabled(new WeaponEnabledEventArgs(WeaponType));
        }

        public virtual void Use()
        {

        }

        public virtual void ReloadWeapon()
        {
            
        }

        public virtual void InterruptReloading()
        {

        }

        public virtual void StartAiming()
        {
            IsAiming = true;
        }

        public virtual void InterruptAiming()
        {
            IsAiming = false;
        }

        protected virtual void PickWeapon()
        {

        }

        public virtual void Discard()
        {
            OnWeaponDiscarded(EventArgs.Empty);
        }

        protected virtual void OnWeaponEnabled(WeaponEnabledEventArgs args)
        {
            if (WeaponEnabled != null)
                WeaponEnabled(this, args);
        }

        protected virtual void OnWeaponDiscarded(EventArgs args)
        {
            if (WeaponDiscarded != null)
                WeaponDiscarded(this, args);
        }

        protected virtual void OnAmmoAmountChanged(AmmoChangedEventArgs args)
        {
            if (AmmoAmountChanged != null)
                AmmoAmountChanged(this, args);
        }
    }
}