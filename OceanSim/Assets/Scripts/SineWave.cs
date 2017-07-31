using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : IWave {

    public float WaveLength = 10.0f;
    public float Speed = 1.0f;
    public float Amplitude = 5.0f;
    public Vector2 Direction = Vector2.left;

    private float _frequency;
    private float _phaseConstant;

    public void UpdateConfiguration() {
        _frequency = 2 / WaveLength;
        _phaseConstant = Speed * _frequency;
    }

    public Vector3 getPositionOffset(float x, float z, float time) {
        var position = new Vector2(x, z);
        var y = Amplitude * Mathf.Sin(Vector2.Dot(Direction, position) * _frequency + 0.0f * _phaseConstant);
        return new Vector3(0.0f, y, 0.0f);
    }
}
