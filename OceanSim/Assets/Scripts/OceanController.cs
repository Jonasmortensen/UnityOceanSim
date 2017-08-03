using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanController : MonoBehaviour {
    //[Range(0, 20)]
    //public int WaveCount;
    private Wave[] waves;

    //public float Amplitude;
    //public float WaveLength;
    //public float Speed;
    public bool UseWaterDispertionAsSpeed;
    //[Range(0.0f, 1.0f)]
    //public float Steepness;
    //public Transform Direction;

    //This is determined by the shader
    private const int WAVEBUFFER = 20;

	// Use this for initialization
	void Start () {
        waves = GetComponentsInChildren<Wave>();
        Debug.Log("There are " + waves.Length + " waves.");
	}
	
	// Update is called once per frame
	void Update () {

        //Vector3 windDirection = Direction.forward.normalized;

        float[] directionXs = new float[WAVEBUFFER];
        float[] directionZs = new float[WAVEBUFFER];
        float[] amplitudes = new float[WAVEBUFFER];
        float[] frequencies = new float[WAVEBUFFER];
        float[] Qs = new float[WAVEBUFFER];
        float[] phaseConstants = new float[WAVEBUFFER];

        for(int i = 0; i < waves.Length; i++) {
            Wave currentWave = waves[i];
            Vector3 direction = -currentWave.transform.forward;
            float frequency = 2.0f / currentWave.WaveLength;

            directionXs[i] = direction.x;
            directionZs[i] = direction.z;
            amplitudes[i] = currentWave.Amplitude;
            frequencies[i] = frequency;
            if(currentWave.Amplitude != 0) {
                Qs[i] = currentWave.Steepness / (frequency * currentWave.Amplitude * waves.Length);
            }
            if (UseWaterDispertionAsSpeed) {
                //phaseConstants[i] = frequency * Mathf.Sqrt(9.8f * ((2 * Mathf.PI) / waves[0].WaveLength));
                phaseConstants[i] = frequency * Mathf.Sqrt(9.8f * ((2 * Mathf.PI) / currentWave.WaveLength));
            } else {
                phaseConstants[i] = frequency * currentWave.Speed;
            }
        }

        Shader.SetGlobalFloat("_WaveTime", Time.time);
        Shader.SetGlobalInt("_WaveCount", waves.Length);
        Shader.SetGlobalFloatArray("_Amplitude", amplitudes);
        Shader.SetGlobalFloatArray("_DirectionX", directionXs);
        Shader.SetGlobalFloatArray("_DirectionZ", directionZs);
        Shader.SetGlobalFloatArray("_Q", Qs);
        Shader.SetGlobalFloatArray("_Frequency", frequencies);
        Shader.SetGlobalFloatArray("_PhaseConstant", phaseConstants);

    }

}
