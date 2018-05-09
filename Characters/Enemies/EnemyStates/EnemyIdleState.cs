using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Characters.Enemies.EnemyStates
{
    public class EnemyIdleState : IState
    {
        readonly Animation animation;
        readonly AnimationClip [] idleAnimations;
        NavMeshAgent agent;

        int randomAnimationIndex;
        readonly float minTimeBetweenTransition = 5f;
        readonly float maxTimeBetweenTransitions = 8f;

        public EnemyIdleState(Animation animation, AnimationClip [] idleAnimations, NavMeshAgent agent)
        {
            this.idleAnimations = idleAnimations;
            this.animation = animation;
            this.agent = agent;
        }

        public void Enter()
        {
            PlayRandomAnimation();
            Debug.Log("Entered Idle State " + animation.gameObject.name);
        }

        public void Execute()
        {
            
        }

        public void Exit()
        {
            
        }

        void PlayRandomAnimation()
        {          
            randomAnimationIndex = Random.Range(0, idleAnimations.Length);
            var randomClip = idleAnimations[randomAnimationIndex];

            Debug.Log("Chosen random animation " + randomAnimationIndex);

            
            animation.AddClip(randomClip, "Idle_" + randomAnimationIndex);        
        }

        IEnumerator BlendAnimations()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minTimeBetweenTransition, maxTimeBetweenTransitions));
            }
        }
    }
}