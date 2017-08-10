using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour, IListener, ILeftAnalogListener, IRightAnalogListener, IButtonDownListener {
    public Wind wind;
    public Mast mast;
    public Rudder rudder;

    private Rigidbody rb;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();

        InputHandler.Instance.SetListenersOrNull(this);
    }

    // Update is called once per frame
    void FixedUpdate() {
        ApplyWindOnSail();

    }

    public void ApplyWindOnSail() {

        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        Vector3 relativeWind = wind.getWindVector() - horizontalVelocity;

        Vector3 sailForce = mast.getSailForce(relativeWind);
        Vector3 boatForce = Vector3.Project(sailForce, transform.forward);

        rb.AddForce(boatForce);


        Vector3 rudderDirection = rudder.getRudderDirection();

        rb.AddForceAtPosition(rudderDirection * Vector3.Dot(rudderDirection, transform.forward) * horizontalVelocity.magnitude, rudder.getRudderForcePoint());

        //For debuggin
        //Vector3 lineStart = new Vector3(transform.position.x - 15, transform.position.y + 5, transform.position.z + 5);

        //Debug.DrawLine(lineStart, lineStart + (sailForce / sailSize) * 5, Color.blue);
        //Debug.DrawLine(lineStart, lineStart + (boatForce / sailSize) * 5, Color.red);
        //Debug.DrawLine(lineStart, lineStart - (relativeWind / windSpeed) * 5, Color.green);
        //End debugging
    }

    public float getSpeed() {
        return Vector3.Project(rb.velocity, transform.forward).magnitude;
    }

    public void Cirkel_down() {
        throw new NotImplementedException();
    }

    public void LeftAnalogPosition(float x, float y) {
        rudder.rotate(-x * rudder.rotationSpeed);
    }

    public void LeftAnalog_down() {
        rudder.resetRotation();
    }

    public void RightAnalogPosition(float x, float y) {
        mast.rotate(-x * mast.rotationSpeed);
    }

    public void RightAnalog_down() {
        mast.resetRotation();
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
