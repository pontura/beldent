using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectData : MonoBehaviour {
	
	public int laneID;
	public types type;
	public Vector3 pos;
	public enum types
	{
		CHARACTER,
		OBSTACLE1,
		GRAB
	}

}
