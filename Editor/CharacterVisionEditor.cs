using Assets.Scripts.Vision;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [CustomEditor (typeof (CharacterVision))]
    public class CharacterVisionEditor : UnityEditor.Editor
    {
        private void OnSceneGUI () {
            CharacterVision vision = target as CharacterVision;
            Handles.color = Color.yellow;

            if (vision != null)
            {
                Handles.DrawWireArc(vision.transform.position, Vector3.up, Vector3.forward, 360, vision.ViewRadius);

                Vector3 viewAngleA = vision.DirectionFromAngle(-vision.ViewAngle / 2f, false);
                Vector3 viewAngleB = vision.DirectionFromAngle(vision.ViewAngle / 2f, false);

                Handles.color = Color.green;
                Handles.DrawLine(vision.transform.position, vision.transform.position + viewAngleA * vision.ViewRadius);
                Handles.DrawLine(vision.transform.position, vision.transform.position + viewAngleB * vision.ViewRadius);

                Handles.color = Color.red;

                foreach (Transform visibleTarget in vision.VisibleTargets)
                {
                    Handles.DrawLine(vision.transform.position, visibleTarget.position);
                }
            }
        }
    }
}