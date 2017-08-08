using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PrintListener : MonoBehaviour, IListener, IDPadDownListener, ILeftAnalogListener, IRightAnalogListener, IButtonDownListener, IBumpersDownListener {

	// Use this for initialization
	void Start () {
        InputHandler.Instance.SetListenersOrNull(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DPadUp_down() {
		Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
	}

	public void DPadDown_down() {
		Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
	}

	public void DPadLeft_down() {
		Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
	}

	public void DPadRight_down() {
		Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
	}


	public void LeftAnalogPosition(float x, float y) {
		
	}

	public void LeftAnalog_down() {
		Debug.Log("Pressed" + MethodBase.GetCurrentMethod().Name);
	}

    public void RightAnalogPosition(float x, float y) {;
    }

    public void RightAnalog_down() {
        Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
    }

    public void X_down() {
        Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
    }

    public void Cirkel_down() {
        Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
    }

    public void Square_down() {
        Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
    }

    public void Trianlgle_down() {
        Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
    }

    public void L1_down() {
        Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
    }

    public void L2_down() {
        Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
    }

    public void R1_down() {
        Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
    }

    public void R2_down() {
        Debug.Log("Pressed: " + MethodBase.GetCurrentMethod().Name);
    }
}
