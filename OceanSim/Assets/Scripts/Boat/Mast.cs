using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Mast : MonoBehaviour {

    public float SailSize;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 getSailForce(Vector3 windOnSail) {
        Vector3 sailDirection = transform.right;

        float sailEffeciency = Vector3.Dot(windOnSail.normalized, sailDirection);

        return sailDirection * sailEffeciency * SailSize * windOnSail.magnitude;
    }
}
