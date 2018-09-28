using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Obstacle {

	public Animator anim_to_instantiate;
	public Character characterToFollow;
	public int id;
	public float distance;

	public Vector3 lanePos;
	float speedY = 0.05f;
	public Animator anim;

	float MaxOffset = 7;
	float offset;
	float offset_z;
	public states state;

	public enum states
	{
		IDLE,
		PLAYING,
		CHANGING_LANE,
		HIT,
		WIN
	}	
	public override void OnPool() 
	{ 
		characterToFollow = null;
		Destroy (anim.gameObject);
	}
	public void InitCharacter (CustomizationData data, Character _characterToFollow, int laneID) {

		float rand = (float)Random.Range (-14, 14) / 10;
		offset_z = rand;

		this.anim = Instantiate (anim_to_instantiate);
		anim.transform.localPosition =new Vector3(0,0,0);
		anim.transform.SetParent (transform);

		anim.GetComponent<AvatarCustomizer> ().Init (data);
		state = states.IDLE;
		anim.transform.localScale = new Vector3 (1, 1, 1);

		this.characterToFollow = _characterToFollow;

		if(characterToFollow == null)
			anim.transform.localScale = new Vector3 (-1, 1, 1);
		
		this.laneID = laneID;
		lanePos = BoardManager.Instance.lanes.GetCoordsByLane (laneID);
		Vector3 pos = transform.localPosition;
		pos.y = lanePos.y;
		pos.z = lanePos.z;
		transform.localPosition = pos;
		anim.transform.SetParent (transform);
		anim.transform.localPosition = Vector3.zero;

		anim.Play ("enemy_idle");
	}
	public void Revive(int laneID)
	{
		float rand = (float)Random.Range (-20, 20) / 10;
		offset += rand;
		StartRunning ();
		this.laneID = laneID;
		lanePos = BoardManager.Instance.lanes.GetCoordsByLane (laneID);
		ForceToLanePosition ();
		anim.Play ("enemy_run");
	}
	public void StartRunning()
	{
		state = states.PLAYING;
		anim.Play ("enemy_run");
	}
	void Update()
	{
		if (state == states.HIT || state == states.IDLE || state == states.WIN)
			return;
		Vector3 pos = transform.localPosition;
		pos.x = distance + offset;
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
		if (state == states.HIT || state == states.IDLE || state == states.WIN)
			yield return null;
		if (state != states.HIT ) {
			float delay = Vector3.Distance (characterToFollow.transform.localPosition, transform.localPosition) / 10;
			yield return new WaitForSeconds (delay);
			if (state == states.HIT || state == states.IDLE || state == states.WIN)
				yield return null;
			state = states.CHANGING_LANE;
			lanePos = BoardManager.Instance.lanes.GetCoordsByLane (laneID);
			lanePos.y = lanePos.y;
			lanePos.z = lanePos.z;
			Invoke ("ForceToLanePosition", 0.3f);
		}
		yield return null;
	}
	void ForceToLanePosition()
	{
		if (state == states.HIT || state == states.IDLE || state == states.WIN)
			return;
		Vector3 pos = transform.localPosition;
		pos.y = lanePos.y;
		pos.z = lanePos.z + offset_z;
		transform.localPosition = pos;
		anim.Play ("enemy_run");
	}
	public void Move(float distance, float offset)
	{
		if (state == states.HIT || state == states.IDLE || state == states.WIN)
			return;
		if( this.offset<MaxOffset )
			this.offset += offset;
		
		this.distance = distance;
	}
	public void StopRunning()
	{
		if (state == states.WIN)
			return;
		state = states.IDLE;
		anim.Play ("enemy_idle");
	}
	public void Win()
	{
		state = states.WIN;
		anim.Play ("enemy_win");
		Vector3 pos = anim.transform.localPosition;
		pos.z -= 2;
		anim.transform.localPosition = pos;
	}
	public void Hit()
	{
		if (state == states.HIT)
			return;
		state = states.HIT;
		anim.Play ("enemy_hit");
		offset = -1;
	}
}
