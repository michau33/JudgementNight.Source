using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    protected static T _instance;

    public static T instance {
        get {
            if (!_instance) {
                _instance = GameObject.FindObjectOfType (typeof (T)) as T;

                if (!_instance) {
                    // string errorMessage = $"Instance of {typeof(T)} is needed in the scene. But there is not";
                    Debug.LogError ("Instance of " + typeof (T) + " is needed in the scene.");
                }
            }

            return _instance;
        }
    }
}