using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanController : MonoBehaviour {
    public float Amplitude;
    public float WaveLength;
    public float Speed;
    [Range(0.0f, 1.0f)]
    public float Steepness;
    public Transform Direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 windDirection = Direction.forward.normalized;

        Shader.SetGlobalFloat("_WaveTime", Time.time);
        Shader.SetGlobalFloat("_Amplitude", Amplitude);
        Shader.SetGlobalFloat("_DirectionX", windDirection.x);
        Shader.SetGlobalFloat("_DirectionZ", windDirection.z);
        var frequency = 2.0f / WaveLength;
        Shader.SetGlobalFloat("_Q", Steepness/(frequency*Amplitude));
        Shader.SetGlobalFloat("_Frequency", frequency);
        Shader.SetGlobalFloat("_PhaseConstant", frequency * Speed);

    }
}
