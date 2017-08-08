using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    public IDPadDownListener DpadDownListener;
	public ILeftAnalogListener LeftAnalogListener;
    public IRightAnalogListener RightAnalogListener;
    public IButtonDownListener ButtonDownListener;
    public IBumpersDownListener BumpersDownListener;

	private bool pressedDpadHori = false;
	private bool pressedDpadVerti = false;

	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

    public static InputHandler Instance { get; private set; }

    // Update is called once per frame
    void Update () {

		if (DpadDownListener != null) {
			listenForDPadDown();
		}

        if(LeftAnalogListener != null) {
            listenForLeftAnalog();
        }

        if(RightAnalogListener != null) {
            listenForRightAnalog();
        }

        if(ButtonDownListener != null) {
            listenForButtons();
        }

        if(BumpersDownListener != null) {
            listenForBumpers();
        }
    }

    public void SetListenersOrNull(IListener listener) {
        if(listener is IDPadDownListener) {
            DpadDownListener = (IDPadDownListener)listener;
            Debug.Log("Yay!");
        } else {
            DpadDownListener = null;
        }

        if (listener is ILeftAnalogListener) {
            LeftAnalogListener = (ILeftAnalogListener)listener;
        }
        else {
            LeftAnalogListener = null;
        }

        if (listener is IRightAnalogListener) {
            RightAnalogListener = (IRightAnalogListener)listener;
        }
        else {
            RightAnalogListener = null;
        }

        if (listener is IButtonDownListener) {
            ButtonDownListener = (IButtonDownListener)listener;
        }
        else {
            ButtonDownListener = null;
        }

        if (listener is IBumpersDownListener) {
            BumpersDownListener = (IBumpersDownListener)listener;
        }
        else {
            BumpersDownListener = null;
        }
    }


	private void listenForLeftAnalog() {
		float x = Input.GetAxis("LeftAnalog_x");
		float y = Input.GetAxis("LeftAnalog_y");

        LeftAnalogListener.LeftAnalogPosition(x, y);
		
		if (Input.GetButtonDown("LeftAnalog_down")) {
			LeftAnalogListener.LeftAnalog_down();
		}
	}

    private void listenForRightAnalog() {
        float x = Input.GetAxis("RightAnalog_x");
        float y = Input.GetAxis("RightAnalog_y");

        RightAnalogListener.RightAnalogPosition(x, y);

        if (Input.GetButtonDown("RightAnalog_down")) {
            RightAnalogListener.RightAnalog_down();
        }
    }

    private void listenForButtons() {
        if (Input.GetButtonDown("X")) {
            ButtonDownListener.X_down();
        }
        if (Input.GetButtonDown("Square")) {
            ButtonDownListener.Square_down();
        }
        if (Input.GetButtonDown("Triangle")) {
            ButtonDownListener.Trianlgle_down();
        }
        if(Input.GetButtonDown("Cirkle")) {
            ButtonDownListener.Cirkel_down();
        }
    }

    private void listenForBumpers() {
        if (Input.GetButtonDown("L1")) {
            BumpersDownListener.L1_down();
        }
        if (Input.GetButtonDown("L2Button")) {
            BumpersDownListener.L2_down();
        }
        if (Input.GetButtonDown("R1")) {
            BumpersDownListener.R1_down();
        }
        if(Input.GetButtonDown("R2Button")) {
            BumpersDownListener.R2_down();
        }
    }

    private void listenForDPadDown() {
	    float horizontal = Input.GetAxis("DPad_horizontal");
	    float vertical = Input.GetAxis("DPad_vertical");
	    
	    if (horizontal < 0.0f && pressedDpadHori == false) {
		    DpadDownListener.DPadLeft_down();
		    pressedDpadHori = true;
	    } else if (horizontal > 0.0f && pressedDpadHori == false) {
		    DpadDownListener.DPadRight_down();
		    pressedDpadHori = true;
	    } else if (horizontal == 0.0f && pressedDpadHori == true) {
		    //Debug.Log("Released dpad");
		    pressedDpadHori = false;
	    }
	    
	    if (vertical < 0.0f && pressedDpadVerti == false) {
		    DpadDownListener.DPadUp_down();
		    pressedDpadVerti = true;
	    } else if (vertical > 0.0f && pressedDpadVerti == false) {
		    DpadDownListener.DPadDown_down();
		    pressedDpadVerti = true;
	    } else if (vertical == 0.0f && pressedDpadVerti == true) {
		    //Debug.Log("Released dpad");
		    pressedDpadVerti = false;
	    }
    }
}
