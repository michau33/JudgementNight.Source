using UnityEngine;

namespace Assets.Scripts.Characters.Enemies.EnemyStates
{
    public class EnemyPatrolState : IState
    {
        private Animator animator;

        public EnemyPatrolState(Animator animator)
        {
            this.animator = animator;
        }

        public void Enter()
        {
            Debug.Log("Entered Patrol State " + animator.gameObject.name);
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