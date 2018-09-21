using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanes : MonoBehaviour {

	public GameObject[] all;

	public Vector3 GetCoordsByLane(int laneID)
	{
		int id = 0;
		foreach (GameObject go in all) {
			if (laneID == id)
				return go.transform.localPosition;
			id++;
		}
		return Vector3.zero;
	}
}
