using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionCam : MonoBehaviour {

    public Camera RenderingCam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 renderCamPos = RenderingCam.transform.position;
        Vector3 renderCamRot = RenderingCam.transform.rotation.eulerAngles;
        Debug.Log("Main cam rotation: " + renderCamRot);

        transform.position = new Vector3(renderCamPos.x, -renderCamPos.y, renderCamPos.z);
        transform.rotation = Quaternion.Euler(new Vector3(-renderCamRot.x, renderCamRot.y, renderCamRot.z));
	}
}
