using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Events.CustomEvents
{
    public class TargetSpottedEvent : IEvent
    {
        public readonly Transform Sender;
        public readonly Collider TargetCollider;

        public TargetSpottedEvent(Collider targetCollider, Transform sender)
        {
            TargetCollider = targetCollider;
            Sender = sender;
        }
    }
}