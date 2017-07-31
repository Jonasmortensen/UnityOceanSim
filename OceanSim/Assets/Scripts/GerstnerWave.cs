using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerstnerWave : MonoBehaviour, IWave {

    public float WaveLength = 4.0f;
    public float Speed = 1.0f;
    public float Amplitude = 1.0f;
    public Vector2 Direction = Vector2.left;

    private float _frequency;
    private float _phaseConstant;
    private float _q;

    public Vector3 getPositionOffset(float x, float z, float time) {
        var position = new Vector2(x, z);
        var constant = Vector2.Dot(Direction, position) * _frequency + time * _phaseConstant;

        var newX = _q * Amplitude * Direction.x * Mathf.Cos(constant);
        var newZ = _q * Amplitude * Direction.y * Mathf.Cos(constant);
        var newY = Amplitude * Mathf.Sin(constant);

        return new Vector3(newX, newY, newZ);
    }

    public void UpdateConfiguration() {
        _frequency = 2 / WaveLength;
        Speed = Mathf.Sqrt(9.8f * ((2 * Mathf.PI) / WaveLength));
        _phaseConstant = Speed * _frequency;
        _q = 0.5f / (_frequency * Amplitude);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
