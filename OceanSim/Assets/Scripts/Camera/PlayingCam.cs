using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayingCam : MonoBehaviour {

    public Transform boat;

    public Transform camPos1;
    public Transform camPos2;

    [Range(0.0f, 1.0f)]
    public float interpolation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(camPos1.position, camPos2.position, interpolation);
        transform.rotation = Quaternion.Lerp(camPos1.rotation, camPos2.rotation, interpolation);
	}
}
