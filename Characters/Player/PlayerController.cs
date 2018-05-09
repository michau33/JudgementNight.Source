using System.Collections;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.Inputs;
using Assets.Scripts.Utility;
using Assets.Scripts.Vision;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(WeaponsCarrying))]
    public class PlayerController : MonoBehaviour, IController, IUseVision
    {
        public ControlType ControlType;
        public Transform VisionPlaceholder;
        public CharacterVision Vision;

        Player player;
        CharacterController controller;
        Animator animator;
        bool isEnemyVisible;

        public PlayerMovement PlayerMovement { get; private set; }
        public WeaponsCarrying WeaponsCarrying { get; private set; }

        void Awake ()
        {
            InputManager.Initialize(ControlType);

            player = GetComponent<Player>();
            controller = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();

            PlayerMovement = GetComponent<PlayerMovement>();
            WeaponsCarrying = GetComponentInChildren<WeaponsCarrying>();

            PlayerMovement.Initialize(controller, player.PlayerStats, ControlType, animator);
        }

        void Start()
        {
            if (Vision != null)
            {
                Vision.VisionOwner = gameObject;
                Vision.transform.position = VisionPlaceholder.position;
            }

            PubSubService.RegisterListener<TargetSpottedEvent>(OnTargetSpotted);
            PubSubService.RegisterListener<TargetLostEvent>(OnTargetLost);
        }

        void Update ()
        {
            HandleWeaponInterractions ();
        }

        void FixedUpdate()
        {
            float horizontal = InputManager.Horizontal();
            float vertical = InputManager.Vertical();

            PlayerMovement.Move(horizontal, vertical);
            PlayerMovement.Rotate();
        }

        /// <summary>
        /// Handles player interaction with weapons i.e. shooting, reloading etc.
        /// </summary>
        void HandleWeaponInterractions ()
        {
            var currentWeapon = WeaponsCarrying.CurrentWeapon;

            if (InputManager.AimingPressed() && !currentWeapon.IsAiming)
            {
                animator.SetBool("isAiming", true);
                currentWeapon.StartAiming();
                PlayerMovement.SlowDown(0.9f);
                Vision.ExtendVision(0.4f, 1f);
            }
            
            if (InputManager.AimingReleased() && currentWeapon.IsAiming)
            {
                animator.SetBool("isAiming", false);
                currentWeapon.InterruptAiming();
                Vision.ResetVisionToDefault(1f);
            }

            if (InputManager.FireButtonHeld() && currentWeapon.IsAiming)
            {
                animator.SetTrigger("isShooting");
                WeaponsCarrying.Shoot();
            }

            if (InputManager.ReloadingButtonHeld()) 
            {
                WeaponsCarrying.Reload();
            } 
            else 
            {
                WeaponsCarrying.StopReloading();
            }
        }

        /// <summary>
        /// This event fires whenever character vision reaveals another player
        /// </summary>
        /// <param name="targetSpottedEvent"></param>
        public void OnTargetSpotted(object targetSpottedEvent)
        {
            var target = ((TargetSpottedEvent) targetSpottedEvent).TargetCollider;
            LayerMask targetLayer = target.gameObject.layer;

            if (!targetLayer.Contains(Layers.Enemy))
                return;
        }

        /// <summary>
        /// This event fires whenever character vision loses target
        /// </summary>
        /// <param name="targetLostEvent"></param>
        public void OnTargetLost(object targetLostEvent)
        {
            var target = ((TargetLostEvent) targetLostEvent).TargetCollider;
            LayerMask targetLayer = target.gameObject.layer;
            
            if (!targetLayer.Contains(Layers.Enemy))
                return;
        }

        public void OnGotSpotted(GameObject spottedBy)
        {

        }

        //IEnumerator HighlightEnemy(GameObject enemyToHighlight)
        //{
        //    // TODO CONSIDER ADDING STATIC LIST OF TAGS  IN GAME AND USE IT LIKE THAT Tags.VisibleEnemy
        //    GameObject parent = enemyToHighlight.transform.parent.gameObject;
        //    int oldLayer = parent.layer;

        //    SetLayerRecursively(parent, LayerMask.NameToLayer("VisibleEnemy"));

        //    yield return new WaitForSeconds(3f);

        //    SetLayerRecursively(parent, oldLayer);
        //}

        //private void SetLayerRecursively(GameObject obj, int newLayer)
        //{
        //    if (obj == null)
        //        return;

        //    obj.layer = newLayer;

        //    foreach (Transform child in obj.transform)
        //    {
        //        // ignore changing layers on character Vision for now.
        //        if (child == null || child.gameObject.GetComponent<CharacterVision>())
        //            continue;

        //        SetLayerRecursively(child.gameObject, newLayer);
        //    }
        //}
    }
}