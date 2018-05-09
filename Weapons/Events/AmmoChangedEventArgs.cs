using System;

namespace Assets.Scripts.Weapons.Events
{
    public class AmmoChangedEventArgs : EventArgs
    {
        readonly int Amount;

        public AmmoChangedEventArgs(int amount)
        {
            Amount = amount;
        }
    }
}