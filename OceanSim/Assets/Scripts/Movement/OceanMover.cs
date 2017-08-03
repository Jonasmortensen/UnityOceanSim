using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanMover : MonoBehaviour {
    public GameObject toFollow;
    public float resolution;

    //public Transform innerOcean;
    //public Transform outerOcean;

    private Vector3 outerOriginalPos;

    private void Start() {
        //innerOcean = transform.Find("InnerOcean");
        //outerOcean = transform.Find("OuterOcean");
        //outerOriginalPos = outerOcean.position;
    }

    // Update is called once per frame
    void Update () {
        moveOceanTo(toFollow.transform);
	}

    private void moveOceanTo(Transform target) {
        //Debug.Log(outerOriginalPos);

        Vector3 targetPos = target.position;
        //transform.position = new Vector3(Mathf.Floor(targetPos.x / resolution) * resolution, transform.position.y, Mathf.Floor(targetPos.z / resolution) * resolution);
        transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
        //outerOcean.transform.position = (new Vector3(0, -0.3f, 240)) + new Vector3(Mathf.Floor(targetPos.x / resolution) * resolution, transform.position.y, Mathf.Floor(targetPos.z / resolution) * resolution);
        //outerOcean.transform.position = outerOriginalPos + new Vector3(Mathf.Floor(targetPos.x / resolution) * resolution, transform.position.y, Mathf.Floor(targetPos.z / resolution) * resolution);
    }
}
