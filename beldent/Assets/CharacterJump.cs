using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour {
	
	public float jumpHeight = 2;
	public float jumpTime = .5f;
	public float doubleJumpHeight = 1.5f;
	public float doubleJumpTime = .25f;

	Character character;
	void Start()
	{
		character = GetComponent<Character> ();
	}

	public void Init(){
		iTween.MoveBy(character.anim.gameObject, iTween.Hash(
			"y", jumpHeight, 
			"easeType", "easeOutCubic",
			"time", jumpTime,
			"oncomplete", "OnUp",
			"oncompletetarget", this.gameObject
		));

	}
	void OnUp()
	{
		iTween.MoveBy(character.anim.gameObject, iTween.Hash(
			"y", -jumpHeight, 
			"easeType", "easeInCubic",
			"time", jumpTime,
			"oncomplete", "Done",
			"oncompletetarget", this.gameObject
		));
	}
	void Done()
	{
		character.JumpEnded ();
	}
	//float startingDoubleJumpHeight;
	public void InitDoubleJump(float _height){
		Vector3 pos = character.anim.transform.localPosition;
		pos.y = _height;
		character.anim.transform.localPosition = pos;
		iTween.Stop (this.gameObject);
		//startingDoubleJumpHeight = character.anim.gameObject.transform.localPosition.y;
		iTween.MoveBy(character.anim.gameObject, iTween.Hash(
			"y", doubleJumpHeight, 
			"easeType", "easeOutCubic",
			"time", doubleJumpTime,
			"oncomplete", "OnUpDouble",
			"oncompletetarget", this.gameObject
		));
	}
	void OnUpDouble()
	{
		iTween.MoveBy(character.anim.gameObject, iTween.Hash(
			"y", -character.anim.gameObject.transform.localPosition.y, 
			"easeType", "easeInCubic",
			"time", jumpTime + doubleJumpTime,
			"oncomplete", "Done",
			"oncompletetarget", this.gameObject
		));
	}
}
