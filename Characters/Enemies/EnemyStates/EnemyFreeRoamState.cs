using UnityEngine;

namespace Assets.Scripts.Characters.Enemies.EnemyStates
{
    public class EnemyFreeRoamState : IState
    {
        private Animator animator;

        public EnemyFreeRoamState(Animator animator)
        {
            this.animator = animator;
        }

        public void Enter()
        {
            Debug.Log("Entered FreeRoam State " + animator.gameObject.name);
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}