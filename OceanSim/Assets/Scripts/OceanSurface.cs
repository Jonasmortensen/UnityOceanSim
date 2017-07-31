using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanSurface : MonoBehaviour {

    public List<IWave> Waves;

    private Mesh _mesh;
    private Vector3[] _originalVertices;
    private Vector3[] _modifiedVertices;
    private float elapsedTime;
    private Matrix4x4 localToWorld;
    private Matrix4x4 worldToLocal;

	// Use this for initialization
	void Start () {
        _mesh = GetComponent<MeshFilter>().mesh;
        _originalVertices = _mesh.vertices;
        _modifiedVertices = _mesh.vertices;

        Waves = new List<IWave>();

        var wave1 = new GerstnerWave();//new SineWave();
        wave1.UpdateConfiguration();

        var wave2 = new GerstnerWave();//new SineWave();
        wave2.WaveLength = 5.0f;
        wave2.Amplitude = 0.25f;
        var direction = new Vector2(-3.0f, -1.0f);
        wave2.Direction = direction.normalized;
        wave2.UpdateConfiguration();

        var wave3 = new GerstnerWave();//new SineWave();
        wave3.WaveLength = 2.0f;
        wave3.Amplitude = 0.1f;
        var direction3 = new Vector2(-3.0f, 2.0f);
        wave3.Direction = direction3.normalized;
        wave3.UpdateConfiguration();

        Waves.Add(wave1);
        Waves.Add(wave2);
        Waves.Add(wave3);
    }
	
	// Update is called once per frame
	void Update () {
        localToWorld = transform.localToWorldMatrix;
        worldToLocal = transform.worldToLocalMatrix;
        elapsedTime += Time.deltaTime;

        updateVertices(_originalVertices, _modifiedVertices, elapsedTime);

        _mesh.vertices = _modifiedVertices;
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
        _mesh.RecalculateTangents();
	}

    private void updateVertices(Vector3[] original, Vector3[] toUpdate, float time) {
        int i = 0;
        while(i < original.Length) {
            Vector3 posOffset = Vector3.zero;
            Vector3 currentVertex = localToWorld.MultiplyPoint3x4(original[i]);

            for (int j = 0; j < Waves.Count; j++) {
                posOffset += Waves[j].getPositionOffset(currentVertex.x, currentVertex.z, time);
            }

            toUpdate[i] = worldToLocal.MultiplyPoint3x4(currentVertex + posOffset);
            i++;
        }
    }
}
