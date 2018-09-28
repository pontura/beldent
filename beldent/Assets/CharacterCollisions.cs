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
			Grabbable g = other.GetComponent<Grabbable> ();
			if (g != null) {
				if (character.action == Character.actions.RUNNING)
					g.GotIt (character);
				return;
			} 
			Enemy enemy =  other.GetComponent<Enemy> ();
			if (character.anim.transform.localPosition.y > obstacle._height) {
				character.DoubleJump (obstacle._height+0.5f);
				if (enemy != null) {
					enemy.HittedOnHead ();
					Events.OnScore (character, 15);
				} else {
					Events.OnScore (character, 25);
				}
			}
			else {				
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
