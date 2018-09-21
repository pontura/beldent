using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreasManager : MonoBehaviour {
	
	public Transform container;
	public SceneObject obstacle;
	public Lanes lanes;
	public Area area;
	public float totalDistance;
	public float screenSize = 20;
	public void Check(float distance)
	{
		if (distance > totalDistance - screenSize)
			Add ();
	}
	public void Add() {
		Area newArea = area;
		foreach (SceneObjectData data in area.GetObjects()) {
			SceneObject so = Instantiate (obstacle);
			Vector3 pos = lanes.GetCoordsByLane (data.laneID);
			pos.x = data.pos.x + totalDistance;
			so.transform.SetParent (container);
			so.transform.localPosition = pos;
			so.Init (data.laneID);
		}
		totalDistance += newArea.length;
	}
}
