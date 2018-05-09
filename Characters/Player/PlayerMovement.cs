using System;
using Assets.Scripts.Inputs;
using Assets.Scripts.ScriptableObjects;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class PlayerMovement : MonoBehaviour
    {
        public float MaxSpeed = 10f;

        CharacterController controller;
        PlayerStats playerStats;
        ControlType controlType;
        Animator animator;

        float forwardVelocity;

        private void Awake()
        {
            forwardVelocity = 0f;
        }

        public void Initialize(CharacterController _controller, PlayerStats _playerStats, ControlType _controlType, Animator _animator)
        {
            this.controller = _controller;
            this.playerStats = _playerStats;
            this.controlType = _controlType;
            this.animator = _animator;
        }

        public void Move(float h, float v)
        {
            animator.SetFloat("velocityX", h);
            animator.SetFloat("velocityY", v);
            
            Vector3 movementVector = new Vector3(h, 0f, v).normalized;
            controller.Move(movementVector * playerStats.MovementSpeed * Time.deltaTime);

            //var movement = new Vector3(h, 0f, v).normalized;
            //var movementInput = new Vector3();

            ////Debug.Log(_sidewaysVelocity + " " + _forwardVelocity);
            //movementInput.x -= Mathf.SmoothDamp(movementInput.x, h, ref sidewaysVelocity, 3f);
            //movementInput.z -= Mathf.SmoothDamp(movementInput.z, v, ref forwardVelocity, 3f);

            //animator.SetFloat("velocityY", forwardVelocity);
            //animator.SetFloat("velocityX", sidewaysVelocity);

            //Vector3 forwardMovement = forwardVelocity * playerStats.MovementSpeed * transform.forward;
            //Vector3 sidewaysMovement = sidewaysVelocity * playerStats.MovementSpeed * transform.right;

            //controller.Move((forwardMovement + sidewaysMovement) * Time.deltaTime);
        }

        public void Rotate()
        {
            if (controlType == ControlType.Keyboard) {
                // creates a plane with normal (1st argument) and goes through point (2nd argument)
                Plane playerPlane = new Plane (Vector3.up, transform.position);
                // gets a ray going from camera through a player position 
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                float hitDistance = 0.0f;

                // intersects a ray with the plane
                if (playerPlane.Raycast (ray, out hitDistance)) {
                    // returns a point at distance units along the ray
                    Vector3 targetPoint = ray.GetPoint (hitDistance);
                    // creates a rotation with the specified forward and upwards direction (Z-axis forward, Y-axis up)
                    Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);

                    targetRotation.x = 0f;
                    targetRotation.z = 0f;

                    // spherically interpolates between current rotation and CurrentTarget rotation by speed
                    transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, playerStats.LookRotationSpeed * Time.deltaTime);
                }
            } else if (controlType == ControlType.Gamepad &&
                       (Mathf.Abs (Inputs.InputManager.RightHorizontal ()) >= .1f || Mathf.Abs (Inputs.InputManager.RightVertical ()) >= .1f)) {
                float rHorizontal = Inputs.InputManager.RightHorizontal ();
                float rVertical = Inputs.InputManager.RightVertical ();

                float angle = Mathf.Atan2 (rVertical, -rHorizontal) * Mathf.Rad2Deg;
                // Quaternion targetRotation = Quaternion.Euler(0f, angle * 90f, 0f);
                // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookRotationSpeed * Time.deltaTime);
                // transform.rotation = Quaternion.Euler(0f, angle, 0f);
                transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0f, angle, 0f), playerStats.AnalogRotationSpeed * Time.deltaTime);
            }
        }

        public void SlowDown(float slowdownFactor)
        {
            forwardVelocity = slowdownFactor * forwardVelocity;
        }
    }
}