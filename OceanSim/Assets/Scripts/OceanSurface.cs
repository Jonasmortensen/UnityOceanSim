using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanSurface : MonoBehaviour {

    public IWave wave;

    private Mesh _mesh;
    private Vector3[] _originalVertices;
    private Vector3[] _modifiedVertices;
    private float elapsedTime;

	// Use this for initialization
	void Start () {
        _mesh = GetComponent<MeshFilter>().mesh;
        _originalVertices = _mesh.vertices;
        _modifiedVertices = _mesh.vertices;

        wave = new GerstnerWave();//new SineWave();
        wave.UpdateConfiguration();
        
	}
	
	// Update is called once per frame
	void Update () {
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
            Vector3 currentVertex = original[i];
            toUpdate[i] = currentVertex + wave.getPositionOffset(currentVertex.x, currentVertex.z, time);
            i++;
        }
    }
}
