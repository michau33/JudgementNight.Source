using UnityEngine;

namespace Assets.Scripts.Characters
{
    [RequireComponent(typeof(Animator))] 
    public class PlayerIK : MonoBehaviour {
        protected Animator Animator;
    
        public bool IkActive = false;
        public Transform RightHandObj = null;
        public Transform LookObj = null;

        private void Start () 
        {
            Animator = GetComponent<Animator>();
        }
    
        //a callback for calculating IK
        private void OnAnimatorIK()
        {
            if(Animator) {
            
                //if the IK is active, set the position and rotation directly to the goal. 
                if(IkActive) {

                    // Set the look target position, if one has been assigned
                    if(LookObj != null) {
                        Animator.SetLookAtWeight(1);
                        Animator.SetLookAtPosition(LookObj.position);
                    }    

                    // Set the right hand target position and rotation, if one has been assigned
                    if(RightHandObj != null) {
                        Animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
                        Animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
                        Animator.SetIKPosition(AvatarIKGoal.RightHand,RightHandObj.position);
                        Animator.SetIKRotation(AvatarIKGoal.RightHand,RightHandObj.rotation);
                    }        
                
                }
            
                //if the IK is not active, set the position and rotation of the hand and head back to the original position
                else {          
                    Animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                    Animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
                    Animator.SetLookAtWeight(0);
                }
            }
        }    
    }
}