using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// TODO ADD WEAPON PICKING ETC
// TODO this script will be probably used by many different entities. So make it expandable and flexible
namespace Assets.Scripts.Weapons
{
    public class WeaponsCarrying : MonoBehaviour
    {
        public Weapon StartWeapon;

        [Header("Transform settings")]
        public Transform PlaceholderLightWeapon;

        public List<Weapon> OwnedWeapons { get; private set; }
        public Weapon CurrentWeapon { get; private set; }
        public int CurrentWeaponIndex { get; private set; }

        void Awake ()
        {
            OwnedWeapons = new List<Weapon>();
        }

        void Start()
        {
            Instantiate(StartWeapon.gameObject, PlaceholderLightWeapon.position, PlaceholderLightWeapon.rotation, PlaceholderLightWeapon);
            RefreshWeapons();

            CurrentWeaponIndex = 0;
            SetActiveWeapon (CurrentWeaponIndex);
        }

        void RefreshWeapons()
        {
            OwnedWeapons.Clear();

            foreach (var weapon in GetComponentsInChildren (typeof (Weapon)))
            {
                var weaponComponent = (weapon as Weapon);

                if (OwnedWeapons.Contains(weaponComponent))
                    return;
           
                OwnedWeapons.Add(weaponComponent);
            }
        }

        public void SetActiveWeapon (int index)
        {
            if (!OwnedWeapons.ElementAtOrDefault(index))
                return;

            CurrentWeapon = OwnedWeapons.ElementAt(index);
            CurrentWeapon.gameObject.SetActive(true);

            var weaponsToDeactivate = OwnedWeapons.Where((weapon, idx) => idx != index);
            foreach (var weapon in weaponsToDeactivate)
            {
                weapon.gameObject.SetActive(false);
            }
        }

        public void Reload()
        {
            if (!CurrentWeapon)
                return;

            CurrentWeapon.ReloadWeapon();
        }

        public void StopReloading()
        {
            if (!CurrentWeapon)
                return;

            CurrentWeapon.InterruptReloading();
        }

        public void Shoot () 
        {
            if (!CurrentWeapon)
                return;
       
            CurrentWeapon.Use();
        }
    }
}