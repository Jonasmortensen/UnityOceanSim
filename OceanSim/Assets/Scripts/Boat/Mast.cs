using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Mast : MonoBehaviour {

    public float SailSize;
    public float rotationSpeed;

    private float rotation;

    public Vector3 getSailForce(Vector3 windOnSail) {
        Vector3 sailDirection = transform.right;

        float sailEffeciency = Vector3.Dot(windOnSail.normalized, sailDirection);

        return sailDirection * sailEffeciency * SailSize * windOnSail.magnitude;
    }

    public void rotate(float degrees) {
        transform.Rotate(Vector3.up, degrees);
        rotation += degrees;
    }

    public void resetRotation() {
        rotate(-rotation);
    }
}
