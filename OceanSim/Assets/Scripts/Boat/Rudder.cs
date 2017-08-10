using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudder : MonoBehaviour {

    public Transform forcePoint;
    public float rotationSpeed;

    private float rotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 getRudderDirection() {
        return forcePoint.forward;
    }

    public Vector3 getRudderForcePoint() {
        return forcePoint.position;
    }

    public void rotate(float degrees) {
        transform.Rotate(Vector3.up, degrees, Space.Self);
        forcePoint.Rotate(Vector3.up, degrees, Space.Self);
        rotation += degrees;
    }

    public void resetRotation() {
        rotate(-rotation);
    }
}
