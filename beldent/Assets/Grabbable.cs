using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : Obstacle {

	Character character;

	public void GotIt(Character character)
	{
		Invoke ("Delayed", 0.15f);
		this.character = character;
	}
	void Update()
	{
		if (character == null)
			return;
		Vector3 dest = character.transform.position;
		dest.y += 2;
		transform.position = Vector3.Lerp (transform.position, dest, 0.1f);
	}
	void Delayed()
	{
		Events.OnScore (character, 10);
		Destroy (this.gameObject);
	}
}
