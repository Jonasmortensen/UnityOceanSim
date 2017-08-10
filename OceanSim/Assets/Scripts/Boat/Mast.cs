using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Mast : MonoBehaviour, IRightAnalogListener {

    public float SailSize;
    public float rotationSpeed;

    private float rotation;

    void Start() {
        InputHandler.Instance.RightAnalogListener = this;
    }

    public Vector3 getSailForce(Vector3 windOnSail) {
        Vector3 sailDirection = transform.right;

        float sailEffeciency = Vector3.Dot(windOnSail.normalized, sailDirection);

        return sailDirection * sailEffeciency * SailSize * windOnSail.magnitude;
    }

    private void rotate(float degrees) {
        transform.Rotate(Vector3.up, degrees);
        rotation += degrees;
    }

    private void resetRotation() {
        rotate(-rotation);
    }

    public void RightAnalogPosition(float x, float y) {
        rotate(-x * rotationSpeed);
    }

    public void RightAnalog_down() {
        resetRotation();
    }
}
