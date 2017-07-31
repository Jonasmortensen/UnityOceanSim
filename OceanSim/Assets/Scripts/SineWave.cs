using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour, IWave {

    public float WaveLength = 1.0f;
    public float Speed = 1.0f;
    public float Amplitude = 1.0f;
    public Vector2 Direction = Vector2.left;

    private float _frequency;
    private float _phaseConstant;

    // Use this for initialization
    void Start () {
		
	}

    public Vector3 getPositionOffset(float x, float z, float time) {
        var position = new Vector2(x, z);
        var y = Amplitude * Mathf.Sin(Vector2.Dot(Direction, position) * _frequency + time * _phaseConstant);
        return new Vector3(x, y, z);
    }
}
