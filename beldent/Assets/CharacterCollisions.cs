using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisions : MonoBehaviour {

	Character character;
	void Start () {
		character = GetComponent<Character> ();
	}
	
	void OnTriggerEnter(Collider other)
	{
		Obstacle obstacle = other.GetComponent<Obstacle> ();
		if (obstacle != null) {
			if (character.anim.transform.localPosition.y > obstacle._height) 
				character.DoubleJump ();
			else
				character.Hit ();
		}

	}
}
