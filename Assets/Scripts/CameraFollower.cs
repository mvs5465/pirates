using UnityEngine;

public class CameraFollower : MonoBehaviour {
    public GameObject target;

    void Update() {
        transform.position = target.transform.position + new Vector3(0,0,-5);
    }
}