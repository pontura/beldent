using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public float player1_x;
	public float player2_x;

	float lastY_p1;
	float lastY_p2;

	void Update () {
		player1_x = Input.GetAxis ("Horizontal1");
		player2_x = Input.GetAxis ("Horizontal2");

		float _lastY_p1 = Input.GetAxis ("Vertical1");
		float _lastY_p2 = Input.GetAxis ("Vertical2");

		if (_lastY_p1 != lastY_p1) {
			lastY_p1 = _lastY_p1;
			if(_lastY_p1 != 0)
				Events.OnJoystickAxisVertical (1, (int)_lastY_p1*-1);
		}

		if (_lastY_p2 != lastY_p2) {
			lastY_p2 = _lastY_p2;
			if(_lastY_p2 != 0)
				Events.OnJoystickAxisVertical (2, (int)_lastY_p2*-1);
		}

		if(Input.GetButtonDown("Fire1"))
			Events.OnButtonClicked(1);
		else if(Input.GetButtonDown("Fire2"))
			Events.OnButtonClicked(2);
	}
}
