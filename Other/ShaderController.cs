using Assets.Scripts.Characters;
using UnityEngine;

namespace Assets.Scripts.Other
{
    public class ShaderController : MonoBehaviour
    {
        public float Radius;
        public float Smoothness;

        private GameObject target;

        private void Awake()
        {
            target = GameObject.FindObjectOfType<Player>().gameObject;
        }

        private void Update()
        {
            Vector4 pos = new Vector4(
                target.transform.position.x,
                target.transform.position.y,
                target.transform.position.z,
                0f);

            Shader.SetGlobalVector("GlobalMask_Position", pos);
            Shader.SetGlobalFloat("GlobalMask_Radius", Mathf.Clamp(Radius, 0, 100));
            Shader.SetGlobalFloat("GlobalMask_Softness", Mathf.Clamp(Smoothness, 0, 100));
        }
    }
}