using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class BlockedBuoyancy : MonoBehaviour {
    public float buoyancy;
    public int FloatingResolution;
    public OceanController ocean;

    private BoxCollider boxCollider;
    private Rigidbody rb;

    private Vector3[] localfloatPoints;

    private Vector3 prBlockBuoyuancyForce;
    private float prBlockDragMultiplyer;
    private float viscosity = 1.0f;

	// Use this for initialization
	void Start () {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        localfloatPoints = getLocalFloatPoints(boxCollider, FloatingResolution);

        prBlockBuoyuancyForce = Vector3.up / (FloatingResolution * FloatingResolution * FloatingResolution) * buoyancy;
        prBlockDragMultiplyer = -1f * viscosity / (FloatingResolution * FloatingResolution * FloatingResolution);
	}
	
	// Update is called once per frame
	void Update () {
        addBuoyancy();
	}

    public int CountPointsUnderWater(Vector3[] points, float waterHeight) {
        int count = 0;
        for(int i = 0; i < points.Length; i++) {
            Vector3 currentPoint = transform.TransformPoint(points[i]);
            if (currentPoint.y < waterHeight) {
                count++;
            }
        }
        return count;
    }

    public void addBuoyancy() {
        List<Vector3> underWaterBlocks = new List<Vector3>();

        for (int i = 0; i < localfloatPoints.Length; i++) {
            Vector3 worldPosBlock = transform.TransformPoint(localfloatPoints[i]);
            if (ocean.heighFunction != null && worldPosBlock.y < ocean.heighFunction(worldPosBlock.x, worldPosBlock.z)) {
                underWaterBlocks.Add(worldPosBlock);
                //Buoyancy
                rb.AddForceAtPosition(prBlockBuoyuancyForce, worldPosBlock);
                //Positional drag
                rb.AddForceAtPosition(prBlockDragMultiplyer * rb.velocity, worldPosBlock);
            }
        }
        //ANgular drag
        rb.AddTorque(rb.angularVelocity * -1 * viscosity * 5);
    }




    private Vector3[] getLocalFloatPoints(BoxCollider collider, int resolution) {
        Vector3[] result = new Vector3[resolution * resolution * resolution];
        Vector3 topLeftBack = collider.center - (collider.size / 2f);
        Vector3 space = collider.size / (float)resolution;
        Vector3 halfSpace = space / 2f;;
        for(int x = 0; x < resolution; x++) {
            for(int y = 0; y < resolution; y++) {
                for(int z  = 0; z < resolution; z++) {
                    Vector3 res1 = new Vector3(topLeftBack.x + space.x * x, topLeftBack.y + space.y * y, topLeftBack.z + space.z * z);
                    
                    result[x + (resolution * y) + (resolution * resolution * z)] = res1 + halfSpace;
                }
            }
        }

        return result;
    }
}
