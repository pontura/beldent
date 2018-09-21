using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour {

	public List<Character> all;

	public Character character_to_instantiate;
	Lanes lanes;

	void Start () {
		lanes = GetComponent<Lanes> ();
		Add (1, 1);
		Add (2, 3);
	}
	public void Add(int avatarID, int laneID)
	{
		Character character = Instantiate(character_to_instantiate);
		character.laneID = laneID;
		character.Init (avatarID, laneID);
		all.Add (character);
	}
	public void Move(float distance)
	{
		foreach (Character character in all)
			character.Move (distance);
	}
}
