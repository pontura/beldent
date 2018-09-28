using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour {

	public SceneObjectData data;
	public int laneID;

	public virtual void Init() { }
	public virtual void OnPool() { }
	public void AddToLane (int laneID) {
		this.laneID = laneID;
	}
}
