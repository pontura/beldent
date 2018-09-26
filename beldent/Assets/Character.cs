using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public int id;
	public int laneID;
	public float distance;
	public Vector3 lanePos;
	float speedY = 0.1f;
	public Animator anim_to_instantiate;
	[HideInInspector]
	public Animator anim;
	CharacterJump characterJump;
	public states state;
	float offset_x;
	public enum states
	{
		RUN,
		CHANGING_LANE,
		CRASH,
		DEAD
	}
	public actions action;
	public enum actions
	{
		RUNNING,
		JUMPING
	}
	void Awake()
	{
		characterJump = GetComponent<CharacterJump> ();
	}
	void Start()
	{
		Events.OnJoystickAxisVertical += OnJoystickAxisVertical;
		Events.OnButtonClicked += OnButtonClicked;
	}
	void OnDestroy()
	{
		Events.OnJoystickAxisVertical -= OnJoystickAxisVertical;
		Events.OnButtonClicked -= OnButtonClicked;
	}
	public void Init (int id, int laneID) {
		this.id = id;
		this.laneID = laneID;
		lanePos = BoardManager.Instance.lanes.GetCoordsByLane (laneID);
		ForceToLanePosition ();
		this.anim = Instantiate (anim_to_instantiate);
		anim.transform.SetParent (transform);
		anim.transform.localPosition = Vector3.zero;
	}
	void Update()
	{
		if (state == states.DEAD || state == states.CRASH)
			return;
		
		Vector3 pos = transform.localPosition;

		if (Input.GetAxis ("Horizontal1") != 0) {
			offset_x += Input.GetAxis ("Horizontal1") / 10;
		}

		pos.x = distance + offset_x;
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
		Events.OnCharacterChangeLane (this, laneID);
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
		Jump ();	
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
	public void Run()
	{
		if (state == states.DEAD || state == states.CRASH)
			return;
		action = actions.RUNNING;
		anim.Play ("avatar_run");
	}
	public void Jump()
	{
		if (state == states.DEAD || state == states.CRASH)
			return;
		if (action != actions.RUNNING)
			return;
		action = actions.JUMPING;
		characterJump.Init ();
		anim.Play ("avatar_jump");
	}
	public void DoubleJump()
	{
		if (state == states.DEAD || state == states.CRASH)
			return;
		if (action != actions.JUMPING)
			return;
		action = actions.JUMPING;
		characterJump.InitDoubleJump ();
	}
	public void JumpEnded()
	{
		if (state == states.DEAD || state == states.CRASH)
			return;
		if (action != actions.JUMPING)
			return;
		Run ();
	}
	public void Hit()
	{
		if (state == states.DEAD || state == states.CRASH)
			return;
		Crash ();
	}
	void Crash()
	{
		anim.Play ("avatar_lose");
		state = states.CRASH;
		Events.OnCrash (id);
	}

}
