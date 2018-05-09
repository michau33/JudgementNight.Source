using UnityEngine;

namespace Assets
{
    public class RenderMask : MonoBehaviour
    {
        [SerializeField] 
        private float textureSize = 0.5f;

        private Camera cam;
        
        private void Awake()
        {
            cam = GetComponent<Camera>();
        }

        private void Start()
        {
            var rt = new RenderTexture((int) (Screen.width * textureSize), (int) (Screen.height * textureSize), 0,
                RenderTextureFormat.R8) {name = "ViewMask"};
            rt.Create();
            cam.targetTexture = rt;
            rt.filterMode = FilterMode.Bilinear;

            Shader.SetGlobalTexture("_ViewMask", rt);
        }
    }
}