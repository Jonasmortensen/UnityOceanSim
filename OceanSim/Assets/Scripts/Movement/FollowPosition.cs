using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour {

    public Transform target;

    public Vector3 offset;

	
	// Update is called once per frame
	void Update () {
        Vector3 pos = target.position;
        transform.position = pos + offset;
        //transform.position = target.position;
	}
}
