using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinePulse : MonoBehaviour {
    public float speed;
    public float amount;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.y = Mathf.Sin(Time.time * speed) * amount;
        transform.position = pos;
		
	}
}
