using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Characters.EnemyStates
{
    public class EnemyChaseState : IState
    {
        readonly Animator animator;
        readonly NavMeshAgent agent;
        readonly Transform target;

        public EnemyChaseState(Animator animator, NavMeshAgent agent, Transform target)
        {
            this.animator = animator;
            this.agent = agent;
            this.target = target;
        }

        public void Enter()
        {

        }

        public void Execute()
        {
            Debug.Log("I'm chasing Target ");
            agent.SetDestination(target.position);
        }

        public void Exit()
        {

        }
    }
}