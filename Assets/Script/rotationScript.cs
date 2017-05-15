using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationScript : MonoBehaviour {

    Transform objectTransform;
    Vector3 rotation;
    public float speed;
    public bool isRotatingLeft;
	// Use this for initialization
	void Start () {
        objectTransform = this.transform;
        if (objectTransform)
            rotation = objectTransform.localEulerAngles;
    }

    // Update is called once per frame
    void Update () {
        if (isRotatingLeft)
        {
            objectTransform.rotation = Quaternion.Slerp(objectTransform.rotation, Quaternion.Euler(0, 0, rotation.z + 1f),
                                speed * Time.deltaTime);
        } else
        {
            objectTransform.rotation = Quaternion.Slerp(objectTransform.rotation, Quaternion.Euler(0, 0, rotation.z - 1f),
                                speed * Time.deltaTime);
        }
        rotation.z = objectTransform.localEulerAngles.z;
    }
}
