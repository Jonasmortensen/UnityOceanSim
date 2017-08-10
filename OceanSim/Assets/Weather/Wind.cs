using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {

    public float windSpeed;

    public Vector3 getWindVector() {
        return transform.forward * windSpeed;
    }
}
