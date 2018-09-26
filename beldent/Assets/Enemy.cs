using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Obstacle {

	public Character characterToFollow;
	public int id;
	public float distance;
	public Vector3 lanePos;
	float speedY = 0.1f;
	public Animator anim;

	public states state;

	public enum states
	{
		IDLE,
		PLAYING,
		CHANGING_LANE
	}

	public void Init (Character characterToFollow, int laneID) {
		state = states.PLAYING;
		anim.transform.localScale = new Vector3 (1, 1, 1);
		anim.Play ("enemy_run");
		this.characterToFollow = characterToFollow;
		this.laneID = laneID;
		lanePos = BoardManager.Instance.lanes.GetCoordsByLane (laneID);
		ForceToLanePosition ();
		anim.transform.SetParent (transform);
		anim.transform.localPosition = Vector3.zero;
	}
	void Update()
	{
		if (state == states.IDLE)
			return;
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
	public IEnumerator ChangeLane(int laneID)
	{
		float delay = Vector3.Distance(characterToFollow.transform.localPosition, transform.localPosition)/10;
		yield return new WaitForSeconds (delay);
		state = states.CHANGING_LANE;
		lanePos = BoardManager.Instance.lanes.GetCoordsByLane (laneID);
		lanePos.y = lanePos.y;
		lanePos.z = lanePos.z;
		Invoke ("ForceToLanePosition", 0.12f);
	}
	void ForceToLanePosition()
	{
		Vector3 pos = transform.localPosition;
		pos.y = lanePos.y;
		pos.z = lanePos.z;
		transform.localPosition = pos;
	}
	public void Move(float distance)
	{
		this.distance = distance;
	}
}
