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
		print (other.name);
		if (obstacle != null) {
			Grabbable g = other.GetComponent<Grabbable> ();
			if (g != null) {
				if (character.action == Character.actions.RUNNING)
					g.GotIt (character);
				return;
			} 
			if (character.anim.transform.localPosition.y > obstacle._height) {
				character.DoubleJump (obstacle._height);
			}
			else {
				Enemy enemy =  other.GetComponent<Enemy> ();
				if (enemy) {
					Events.OnAvatarCatched (character);
					if(enemy.characterToFollow == null || enemy.characterToFollow ==character)
						enemy.Win ();
				}
				character.Hit ();
			}
		}

	}
}
