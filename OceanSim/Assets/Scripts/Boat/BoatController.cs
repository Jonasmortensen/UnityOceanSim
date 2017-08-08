using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour, IListener, ILeftAnalogListener, IRightAnalogListener, IButtonDownListener {
    public float mastRotationSpeed;
    public Transform windDirection;
    public float windSpeed;

    private Vector3 windVector;

    private Transform Mast;
    private Transform Rudder;


    // Use this for initialization
    void Start() {

        Mast = transform.Find("MastControl");
        Rudder = transform.Find("RudderControl");
        windVector = windDirection.forward * windSpeed;

        InputHandler.Instance.SetListenersOrNull(this);
    }

    // Update is called once per frame
    void Update() {
        ApplyWindOnSail();

    }

    public void ApplyWindOnSail() {
        Vector3 sailDirection = Mast.right;
        Vector3 sailForce = Vector3.Project(windVector, sailDirection);

        Debug.Log(sailForce);
    }

    public void Cirkel_down() {
        throw new NotImplementedException();
    }

    public void LeftAnalogPosition(float x, float y) {
        Rudder.Rotate(Vector3.up, -x, Space.Self);
    }

    public void LeftAnalog_down() {
        throw new NotImplementedException();
    }

    public void RightAnalogPosition(float x, float y) {
        Mast.Rotate(Vector3.up, -x * mastRotationSpeed);
    }

    public void RightAnalog_down() {
        throw new NotImplementedException();
    }

    public void Square_down() {
        throw new NotImplementedException();
    }

    public void Trianlgle_down() {
        throw new NotImplementedException();
    }

    public void X_down() {
        throw new NotImplementedException();
    }

}
