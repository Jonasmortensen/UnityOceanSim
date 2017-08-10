using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public BoatController boat;


    public Text BoatSpeedText;
    public Text WindSpeedText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        BoatSpeedText.text = "Boat speed: " + boat.getSpeed();
	}
}
