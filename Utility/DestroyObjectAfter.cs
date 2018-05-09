using System.Collections;
using UnityEngine;

public class DestroyObjectAfter : MonoBehaviour {
    [SerializeField] float seconds;

    float time = 0f;

    void Update () {
        time += Time.deltaTime;

        if (time >= seconds) {
            Destroy (this.gameObject);
        }
    }
}