 using UnityEngine;

 public class FreezeRotation : MonoBehaviour
 {
	 Quaternion initialRotation;

	 void Awake()
	 {
		initialRotation = transform.rotation;	 
	 }

	 void LateUpdate()
	 {
		transform.rotation = initialRotation;	 
	 }
 }