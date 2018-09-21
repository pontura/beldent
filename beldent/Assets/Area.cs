using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

	public float length = 19.2f;
	public List<SceneObjectData> all;

	public List<SceneObjectData> GetObjects()
	{
		if (all.Count > 0)
			return all;
		else {
			Lanes lanes = GetComponent<Lanes> ();
			int laneID = 0;
			foreach(GameObject lane in lanes.all)
			{
				foreach (SceneObjectData data in lane.GetComponentsInChildren<SceneObjectData>()) {
					data.laneID = laneID;
					data.pos = data.transform.position;
					all.Add (data);
				}
				laneID++;
			}
		}
		return all;
	}
}
