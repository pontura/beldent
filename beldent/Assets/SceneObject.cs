﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour {

	public int laneID;
	public void Init (int laneID) {
		this.laneID = laneID;
	}
}