using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

	public void Move (float value) {
		Vector2 pos = transform.localPosition;
		pos.x = value;
		transform.localPosition = pos;
	}
}
