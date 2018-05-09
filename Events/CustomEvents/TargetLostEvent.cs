using UnityEngine;

namespace Assets.Scripts.Events.CustomEvents
{
    public class TargetLostEvent : IEvent
    {
        public readonly Collider TargetCollider;

        public TargetLostEvent(Collider targetCollider)
        {
            TargetCollider = targetCollider;
        }
    }
}