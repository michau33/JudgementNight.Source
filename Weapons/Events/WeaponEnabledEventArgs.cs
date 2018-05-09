using System;

namespace Assets.Scripts.Weapons.Events
{
    public class WeaponEnabledEventArgs : EventArgs
    {
        readonly WeaponType WeaponType;

        public WeaponEnabledEventArgs(WeaponType weaponType)
        {
            WeaponType = weaponType;
        }
    }
}