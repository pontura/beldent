using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public int id;
	public int laneID;
	public float distance;
	public Vector3 lanePos;
	float speedY = 0.1f;

	public states state;
	public enum states
	{
		RUN,
		CHANGING_LANE,
		CRASH,
		DEAD
	}

	void Start()
	{
		Events.OnButtonClicked += OnButtonClicked;
		Events.OnJoystickAxisVertical += OnJoystickAxisVertical;
	}
	void OnDestroy()
	{
		Events.OnButtonClicked -= OnButtonClicked;
		Events.OnJoystickAxisVertical -= OnJoystickAxisVertical;
	}
	public void Init (int id, int laneID) {
		this.id = id;
		this.laneID = laneID;
		lanePos = BoardManager.Instance.lanes.GetCoordsByLane (laneID);
		ForceToLanePosition ();
	}
	void Update()
	{
		Vector3 pos = transform.localPosition;
		pos.x = distance;
		if (state == states.CHANGING_LANE) {			
			if (pos.y > lanePos.y)
				pos.y -= speedY;
			else if (pos.y < lanePos.y)
				pos.y += speedY;

			if (pos.z > lanePos.z)
				pos.z -= speedY;
			else if (pos.z < lanePos.z)
				pos.y += speedY;
		}
		transform.localPosition = pos;
	}
	public void ChangeLane()
	{
		state = states.CHANGING_LANE;
		lanePos = BoardManager.Instance.lanes.GetCoordsByLane (laneID);
		lanePos.y = lanePos.y;
		lanePos.z = lanePos.z;
		Invoke ("ForceToLanePosition", 0.12f);
	}
	void ForceToLanePosition()
	{
		if (state == states.DEAD || state == states.CRASH)
			return;
		state = states.RUN;
		Vector3 pos = transform.localPosition;
		pos.y = lanePos.y;
		pos.z = lanePos.z;
		transform.localPosition = pos;
	}
	void OnButtonClicked(int playerID)
	{
		if (playerID != id)
			return;
		print ("jump" + id);
	}

	public void Move(float distance)
	{
		this.distance = distance;
	}

	void OnJoystickAxisVertical(int playerID, int value)
	{		
		if (playerID != id)
			return;
		if (state == states.DEAD || state == states.CRASH || state == states.CHANGING_LANE)
			return;
		this.laneID += value;
		if (laneID < 0) {
			laneID = 0; 
		} else if (laneID > 6) {
			laneID = 6;
		} else
	 		ChangeLane ();
	}

}
