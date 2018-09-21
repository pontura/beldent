using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreasManager : MonoBehaviour {

	public List<float> all_x_positions;

	public Transform container;
	public Area area;
	public float totalDistance;
	public float screenSize = 20;
	public void Check(float distance)
	{
		if (distance > totalDistance - screenSize)
			Add ();
	}
	public void Add() {
		Area newArea = Instantiate(area);
		newArea.transform.SetParent (container);
		newArea.transform.localPosition = new Vector2 (totalDistance, 0);
		totalDistance += newArea.length;
		all_x_positions.Add (totalDistance);
		CheckToRemove ();
	}
	void CheckToRemove()
	{
		foreach (float _x in all_x_positions) {
			if (_x < totalDistance-(screenSize*2)) {
				all_x_positions.RemoveAt (0);
				Area area = container.GetComponentInChildren<Area> ();
				Destroy (area.gameObject);
				return;
			}
		}
				
	}
}
