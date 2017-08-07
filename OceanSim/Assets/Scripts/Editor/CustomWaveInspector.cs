using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Wave))]
public class CustomWaveInspector : Editor {
    Wave wave;
    OceanController parent;

    float amplitude;
    float waveLength;
    float speed;
    float steepness;

    private void OnEnable() {
        wave = (Wave)target;
        parent = wave.transform.parent.GetComponent<OceanController>();

        amplitude = wave.Amplitude;
        waveLength = wave.WaveLength;
        speed = wave.Speed;
        steepness = wave.Steepness;
    }

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();

        amplitude = EditorGUILayout.FloatField("Amplitude", amplitude);
        waveLength = EditorGUILayout.FloatField("Wave Length", waveLength);
        speed = EditorGUILayout.FloatField("Speed", speed);
        steepness = EditorGUILayout.Slider("Steepness", steepness, 0.0f, 1.0f);

        if (GUI.changed) {
            Debug.Log("Something changed! amp = " + amplitude);
            updateWave();
        }

        if (wave.transform.hasChanged) {
            updateWave();
        }
    }

    public void updateWave() {
        wave.Amplitude = amplitude;
        wave.WaveLength = waveLength;
        wave.Speed = speed;
        wave.Steepness = steepness;

        parent.UpdateShader();
    }

}
