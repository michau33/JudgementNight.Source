using Assets.Scripts.Cameras;
using UnityEngine;

namespace Assets.Scripts.Weapons.Ranged
{
    public class GunLaser : MonoBehaviour
    {
        LineRenderer lineRenderer;
        Vector3 cursorPosition;
        Gun attachedGun;

        void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            attachedGun = GetComponent<Gun>();
        }

        void Update()
        {
            var mouseWorldPos = CameraController.instance.GetCursorWorldPosition(attachedGun.FirePoint.position);

            lineRenderer.SetPosition(0, attachedGun.FirePoint.position);
            lineRenderer.SetPosition(1, mouseWorldPos);
        }
    }
}