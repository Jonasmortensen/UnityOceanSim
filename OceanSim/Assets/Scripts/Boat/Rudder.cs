using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudder : MonoBehaviour, ILeftAnalogListener {

    public Transform forcePoint;
    public float rotationSpeed;

    private float rotation;

	void Start () {
        InputHandler.Instance.LeftAnalogListener = this;
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

    public void LeftAnalogPosition(float x, float y) {
        rotate(-x * rotationSpeed);
    }

    public void LeftAnalog_down() {
        resetRotation();
    }
}
