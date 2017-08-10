using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour, IListener, ILeftAnalogListener, IRightAnalogListener, IButtonDownListener {
    public float mastRotationSpeed;
    public float rudderRotationSpeed;
    public Transform windDirection;
    public float windSpeed;
    public float sailSize;

    private Vector3 windVector;
    private Rigidbody rb;

    private Transform Mast;
    private Transform Rudder;
    private Transform RudderForcePoint;

    private float mastRotation;
    private float rudderRotation;


    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();

        Mast = transform.Find("MastControl");
        Rudder = transform.Find("RudderControl");
        RudderForcePoint = transform.Find("RudderForcePoint");
        windVector = windDirection.forward * windSpeed;

        InputHandler.Instance.SetListenersOrNull(this);
    }

    // Update is called once per frame
    void FixedUpdate() {
        ApplyWindOnSail();

    }

    public void ApplyWindOnSail() {

        //Force on boat from sail
        Vector3 sailDirection = Mast.right;
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        Vector3 relativeWind = (windDirection.forward * windSpeed) - horizontalVelocity;
        

        float sailEffeciency = Vector3.Dot(relativeWind.normalized, sailDirection);

        Vector3 sailForce = sailDirection * sailEffeciency * sailSize * relativeWind.magnitude;
        Vector3 boatForce = Vector3.Project(sailForce, transform.forward);

        rb.AddForce(boatForce);

        Debug.Log("Magnitude of force on boat: " + boatForce.magnitude);

        //Force from rudder in water
        rb.AddForceAtPosition(RudderForcePoint.forward * Vector3.Dot(RudderForcePoint.forward, transform.forward) * horizontalVelocity.magnitude, RudderForcePoint.position);

        //For debuggin
        Vector3 lineStart = new Vector3(transform.position.x - 15, transform.position.y + 5, transform.position.z + 5);

        Debug.DrawLine(lineStart, lineStart + (sailForce / sailSize) * 5, Color.blue);
        Debug.DrawLine(lineStart, lineStart + (boatForce / sailSize) * 5, Color.red);
        Debug.DrawLine(lineStart, lineStart - (relativeWind / windSpeed) * 5, Color.green);
        //End debugging
    }

    public float getSpeed() {
        return Vector3.Project(rb.velocity, transform.forward).magnitude;
    }

    public void Cirkel_down() {
        throw new NotImplementedException();
    }

    public void LeftAnalogPosition(float x, float y) {
        float rotation = -x * rudderRotationSpeed;
        Rudder.Rotate(Vector3.up, rotation, Space.Self);
        RudderForcePoint.Rotate(Vector3.up, rotation, Space.Self);
        rudderRotation += rotation;
    }

    public void LeftAnalog_down() {
        Rudder.Rotate(Vector3.up, -rudderRotation, Space.Self);
        RudderForcePoint.Rotate(Vector3.up, -rudderRotation, Space.Self);
        rudderRotation = 0;
    }

    public void RightAnalogPosition(float x, float y) {
        float rotation = -x * mastRotationSpeed;
        Mast.Rotate(Vector3.up, rotation);
        mastRotation += rotation;
    }

    public void RightAnalog_down() {
        Mast.Rotate(Vector3.up, -mastRotation);
        mastRotation = 0;
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
