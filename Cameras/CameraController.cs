using UnityEngine;

namespace Assets.Scripts.Cameras
{
    public class CameraController : Singleton<CameraController> {

        [SerializeField] Transform target;
        [SerializeField] float smoothSpeed = 0.3f;
        [SerializeField] Vector3 offset;

        Vector3 velocity = Vector3.zero;

        void Start()
        {
            Cursor.visible = false;    
        }

        void LateUpdate() 
        {
            if (target) {
                Vector3 targetPos = target.position + offset;
                transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref velocity, smoothSpeed);
                transform.LookAt (target);
            }
        }
        
        /// <summary>
        /// This method helps handling raycast i.e. when you shoot weapon, use laser. You need to pass object position to create plane in the same height as object
        /// to avoid innacurate calculations
        /// </summary>
        /// <param name="objectPosition"></param>
        public Vector3 GetCursorWorldPosition(Vector3 objectPosition)
        {
            Vector3 cursorPosition = Vector3.zero;
            Plane plane = new Plane(Vector3.up, objectPosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDist = 0.0f;

            if (plane.Raycast(ray, out hitDist))
            {
                cursorPosition = ray.GetPoint(hitDist);
            }

            return cursorPosition;
        }
    }
}