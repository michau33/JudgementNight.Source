using UnityEngine;

namespace Assets.Scripts.Vision
{
    public interface IUseVision
    {
        void OnTargetSpotted(object targetSpottedEvent);
        void OnTargetLost(object targetLostEvent);
    }
}