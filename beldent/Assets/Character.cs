using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public int id;
	//public int laneID;
	public float distance;
	public Vector3 lanePos;
	float speedY = 0.07f;
	float delayToRepositionate  = 0.2f;
	public Animator anim_to_instantiate;
	[HideInInspector]
	public Animator anim;
	CharacterJump characterJump;
	public states state;
	float offset_x;
	public float offset_z;
	public enum states
	{	
		IDLE,
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
		//Events.OnJoystickAxisVertical += OnJoystickAxisVertical;
		Events.OnJoystickAxisVerticalRelease += OnJoystickAxisVerticalRelease;
		Events.OnButtonClicked += OnButtonClicked;
	}
	void OnDestroy()
	{
		//Events.OnJoystickAxisVertical -= OnJoystickAxisVertical;
		Events.OnJoystickAxisVerticalRelease -= OnJoystickAxisVerticalRelease;
		Events.OnButtonClicked -= OnButtonClicked;
	}
	public void Init (CustomizationData data, int id, int laneID) {
		this.id = id;
		//this.laneID = laneID;

		this.anim = Instantiate (anim_to_instantiate);
		anim.GetComponent<AvatarCustomizer> ().Init (data);
		offset_x  = -3;
		transform.localPosition = new Vector3(offset_x,0,0);
		anim.transform.localPosition =new Vector3(offset_x,0,0);
		anim.transform.SetParent (transform);
		anim.transform.localScale = Vector3.one;
		lanePos = BoardManager.Instance.lanes.GetCoordsByLane (laneID);
		ForceToLanePosition ();

	}
	void Update()
	{
		if (state == states.IDLE || state == states.DEAD || state == states.CRASH)
			return;
		
		Vector3 pos = transform.localPosition;

		//if (Input.GetAxis ("Vertical" + id) != 0) 
			offset_z = Input.GetAxis ("Vertical" + id)/10;
		


		if (Input.GetAxis ("Horizontal" + id) != 0) {
			offset_x += Input.GetAxis ("Horizontal" + id) / 10;
		}

		pos.x = distance + offset_x;
		pos.y +=  offset_z;
		pos.z +=  offset_z;
//		if (state == states.CHANGING_LANE) {			
//			if (pos.y > lanePos.y)
//				pos.y -= speedY;
//			else if (pos.y < lanePos.y)
//				pos.y += speedY;
//
//			if (pos.z > lanePos.z)
//				pos.z -= speedY;
//			else if (pos.z < lanePos.z)
//				pos.z += speedY;
//		}
		if (pos.y > 1.8f)
		{
			pos.y = 1.8f;
			pos.z = 0;
		} else if (pos.y < -4.3f)
		{
			pos.y = -4.3f;
			pos.z = -6f;
		}
		transform.localPosition = pos;
	}
	public void StartRunning()
	{
		state = states.RUN;
		action = actions.RUNNING;
		anim.Play ("avatar_run");
	}
	public void ChangeLane()
	{
		Events.OnSoundFX ("changeLane");
		state = states.CHANGING_LANE;
		//lanePos = BoardManager.Instance.lanes.GetCoordsByLane (laneID);
		lanePos.y = lanePos.y;
		lanePos.z = lanePos.z;
		Invoke ("ForceToLanePosition", delayToRepositionate);
		Events.OnCharacterChangeLane (this, transform.localPosition);
	}
	void ForceToLanePosition()
	{
		if (state == states.DEAD || state == states.CRASH)
			return;
//		if (preesingVerticalAxis) {
//			state = states.RUN;
//			OnJoystickAxisVertical (id, laneValueMove);
//			return;
//		}
		if(state == states.CHANGING_LANE)
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
	public bool preesingVerticalAxis;
	void OnJoystickAxisVerticalRelease(int playerID)
	{
		return;
		if (playerID != id)
			return;
		preesingVerticalAxis = false;

		print (lanePos.y);
		ForceToLanePosition ();
	}
	int laneValueMove;
	void OnJoystickAxisVertical(int playerID, int laneValueMove)
	{		
//		if (playerID != id)
//			return;
//		if (state == states.IDLE || state == states.DEAD || state == states.CRASH || state == states.CHANGING_LANE)
//			return;
//		this.laneValueMove = laneValueMove;
//		preesingVerticalAxis = true;
//		this.laneID += laneValueMove;
//		if (laneID < 0) {
//			laneID = 0; 
//		} else if (laneID > 6) {
//			laneID = 6;
//		} else
//	 		ChangeLane ();
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
		if (state == states.IDLE || state == states.DEAD || state == states.CRASH)
			return;
		if (action != actions.RUNNING)
			return;
		Events.OnSoundFX ("jump");
		action = actions.JUMPING;
		characterJump.Init ();
		anim.Play ("avatar_jump");
	}
	public void DoubleJump(float _height)
	{
		if (state == states.DEAD || state == states.CRASH)
			return;
		if (action != actions.JUMPING)
			return;
		Events.OnSoundFX ("jump");
		action = actions.JUMPING;
		characterJump.InitDoubleJump (_height);
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
		if (state == states.IDLE || state == states.DEAD || state == states.CRASH)
			return;
		Crash ();
	}
	void Crash()
	{
		Events.OnSoundFX ("crash");
		anim.Play ("avatar_lose");
		state = states.CRASH;
		Events.OnCrash (id);
	}

}
