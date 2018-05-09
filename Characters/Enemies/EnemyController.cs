using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Assets.Scripts.Characters.EnemyStates;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.Utility;
using Assets.Scripts.Vision;

namespace Assets.Scripts.Characters
{
    public class EnemyController : MonoBehaviour, IController, IUseVision
    {
        public CharacterVision Vision;
        public Transform VisionPlaceholder;

        Enemy enemy;
        Animator animator;
        Animation animation;
        NavMeshAgent agent;
        IEnumerator hunchCouroutine;
        readonly StateMachine stateMachine = new StateMachine();

        public IState CurrentState {get { return stateMachine.GetCurrentState(); } }
        public Collider CurrentTarget { get; private set; }

        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            animation = GetComponentInChildren<Animation>();
            agent = GetComponent<NavMeshAgent>();
            enemy = GetComponent<Enemy>();
            Vision = GetComponentInChildren<CharacterVision>();
        }

        void Start()
        {
            CurrentTarget = null;
            agent.speed = enemy.EnemyStats.MovementSpeed;

            //stateMachine.ChangeState(new EnemyIdleState(animation, new AnimationClip[1],  agent));

            // Setup vision parameters and subscribe to events;
            if (Vision != null)
            {
                Vision.VisionOwner = gameObject;
                Vision.transform.position = VisionPlaceholder.position;
                Vision.transform.parent = VisionPlaceholder;
            }
      
            PubSubService.RegisterListener<TargetSpottedEvent>(OnTargetSpotted);
            PubSubService.RegisterListener<TargetLostEvent>(OnTargetLost);
        }

        void Update()
        {
            if (stateMachine.GetCurrentState() != null)
                stateMachine.ExecuteState();
        }

        /// <summary>
        /// This event fires whenever character vision reaveals another player
        /// </summary>
        /// <param name="targetSpottedEvent"></param>
        public void OnTargetSpotted(object targetSpottedEvent)
        {
            var target = ((TargetSpottedEvent) targetSpottedEvent).TargetCollider;
            LayerMask targetLayer = target.gameObject.layer;

            if (!targetLayer.Contains(Layers.Player))
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

            if (!targetLayer.Contains(Layers.Player))
                return;

            OnGotSpotted(target.transform);

        }

        /// <summary>
        /// This fires whenever enemy got spotted by the player
        /// </summary>
        /// <param name="spottedBy"></param>
        public void OnGotSpotted(Transform spottedBy)
        {
            if (hunchCouroutine != null)
                return;

            var randomTime = Random.Range(5f, 10f);
            hunchCouroutine = HunchCounter(spottedBy, randomTime);
            StartCoroutine(hunchCouroutine);
        }

        /// <summary>
        /// Whenever enemy is being observed it gets hunch and after some random time it looks for player 
        /// </summary>
        /// <param name="spottedBy"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        IEnumerator HunchCounter(Transform spottedBy, float time)
        {
            Debug.Log("HMMMM I HAVE A STRANGE GUT FEELING. Probably spotted by " + spottedBy.name);

            float elapsedTime = 0.0f,
                initialDistance = Vector3.Distance(transform.position, spottedBy.position);
            
            float distance = initialDistance;
           
            while (elapsedTime <= time && (distance >= initialDistance * 0.65f))
            {
                elapsedTime += Time.deltaTime;
                distance = Vector3.Distance(transform.position, spottedBy.position);

                yield return null;
            }

            EnterChaseState(spottedBy);
        }

        /// <summary>
        /// Checks if the current state is not ChaseState
        /// </summary>
        /// <param name="target"></param>
        void EnterChaseState(Transform target)
        {
            if (CurrentState == typeof(EnemyChaseState))
                return;

            stateMachine.ChangeState(new EnemyChaseState(animator, agent, target));
        }

        void RotateTowards(Transform target)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 30f);
        }
    }
}