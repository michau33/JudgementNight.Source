using UnityEngine;

namespace Assets.Scripts.Vision
{
    public class CharacterVisionIndicator : MonoBehaviour
    {
        [Header("Parent FOV")]
        public CharacterVision CharacterVision;

        public Light Light { get; private set; }

        private void Awake()
        {
            CharacterVision = GetComponentInParent<CharacterVision>();
            Light = GetComponent<Light>();

            if (CharacterVision == null)
                Debug.LogError("FOV indicator can't work without FOV script attached to the parent.");
        }
        private void Start()
        {
            if (Light)
                return;

            if (Light.type == LightType.Spot)
            {
                Light.spotAngle = CharacterVision.ViewAngle;
                Light.range = CharacterVision.ViewRadius;
            }
            else if (Light.type == LightType.Point)
            {
                // todo consider changing
                //_light.range = _fieldOfView.ViewRadius;
            }
        }
    }
}