using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingObject : MonoBehaviour {
    public float buoyancy;
    private Rigidbody rigidBody;
    private new BoxCollider collider;

    private Vector3[] corners;

    private float viscosity = 1.0f;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();

        //rigidBody.AddForce(new Vector3(300, 0, 0));
        rigidBody.AddForce(new Vector3(0, -20, 0));


    }
	
	// Update is called once per frame
	void FixedUpdate () {

        corners = getBoxColliderCorners(collider);

        List<Vector3> cornersUnder = new List<Vector3>();
        for(int i = 0; i < corners.Length; i++) {
            if (corners[i].y < 0.1f) {
                cornersUnder.Add(corners[i]);
            }
        }

        if(cornersUnder.Count > 0) {
            addWaterForce(getCenterOfPoints(cornersUnder), cornersUnder.Count);
        }
	}

    public void addWaterForce(Vector3 forcePosition, int corners) {
        Vector3 force = Vector3.up * buoyancy * (corners / 8.0f);
        Vector3 posDrag = rigidBody.velocity * -1 * viscosity;
        Vector3 rotDrag = rigidBody.angularVelocity * -1 * viscosity * 2;

        if (force.magnitude > 5.0) {
            Debug.Log("Adding force og magnitude: " + force.magnitude);
            rigidBody.AddForceAtPosition(force, forcePosition);
        }
        rigidBody.AddForceAtPosition(posDrag, forcePosition);
        rigidBody.AddTorque(rotDrag);
    }

    private Vector3[] getBoxColliderCorners(BoxCollider collider) {
        Vector3[] result = new Vector3[8];
        Vector3 halfSize = collider.size / 2f;
        result[0] = transform.TransformPoint(collider.center + halfSize);
        result[1] = transform.TransformPoint(collider.center + new Vector3(halfSize.x, halfSize.y, -halfSize.z));
        result[2] = transform.TransformPoint(collider.center + new Vector3(-halfSize.x, halfSize.y, halfSize.z));
        result[3] = transform.TransformPoint(collider.center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z));
        result[4] = transform.TransformPoint(collider.center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z));
        result[5] = transform.TransformPoint(collider.center + new Vector3(halfSize.x, -halfSize.y, halfSize.z));
        result[6] = transform.TransformPoint(collider.center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z));
        result[7] = transform.TransformPoint(collider.center - halfSize);

        return result;
    }

    private Vector3 getCenterOfPoints(List<Vector3> points) {
        Vector3 center = points[0];
        for (int i = 1; i < points.Count; i++) {
            center += points[i];
        }
        return center / points.Count;
    }

}
