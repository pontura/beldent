using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour {

	public List<Character> all;

	public Character character_to_instantiate;
	Lanes lanes;

	void Start () {
		Events.OnCrash += OnCrash;
		lanes = GetComponent<Lanes> ();
		Add (1, 0);
		Add (2, 3);
	}
	void OnDestroy () {
		Events.OnCrash -= OnCrash;

	}

	Character GetCharacter(int id)
	{
		foreach (Character character in all)
			if (character.id == id)
				return character;
		return null;
	}
	void OnCrash(int avatarID)
	{
		all.Remove (GetCharacter(avatarID));
		if (all.Count == 0)
			Events.OnGameOver ();
	}
	public void Add(int avatarID, int laneID)
	{
		Character character = Instantiate(character_to_instantiate);
		character.laneID = laneID;
		character.Init (avatarID, laneID);
		all.Add (character);
		Events.AddFollower (character);
	}
	public void Move(float distance)
	{
		foreach (Character character in all)
			character.Move (distance);
	}
}
