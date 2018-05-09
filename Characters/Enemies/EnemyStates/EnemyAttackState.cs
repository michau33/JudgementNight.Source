using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Characters.Enemies.EnemyStates
{
    // TODO differentiate logic based on attack type
    public class EnemyAttackState : IState
    {
        private Animator animator;
        private WeaponType weaponType;
        
        public EnemyAttackState(WeaponType weaponType)
        {
            this.weaponType = weaponType;
        }

        public void Enter()
        {
            Debug.Log("Entered Use State " + animator.gameObject.name + " with type of weapon: " + weaponType);
        }

        public void Execute()
        {
            //TODO perform different things depending on weapon type;
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}