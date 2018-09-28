using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour {

	public List<Character> all;

	public Character character_to_instantiate;
	Lanes lanes;

	void Start () {	
		Events.OnCrash += OnCrash;
		Events.OnGameStart += OnGameStart;	
		lanes = GetComponent<Lanes> ();

		Add (1, 2);
		Add (2, 4);

		AddNewFollowers();
	}
	void AddNewFollowers()
	{
		foreach (Character character in all) {
			Events.AddFollower (character);
		}
		Invoke ("AddNewFollowers", 15);
	}
	void OnDestroy () {
		Events.OnCrash -= OnCrash;
		Events.OnGameStart -= OnGameStart;
	}
	void OnGameStart()
	{		
		foreach (Character character in all)
			character.StartRunning ();
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
		CustomizationData cData = Data.Instance.GetComponent<LastCustomizations> ().player1;
		if (avatarID == 2)
			cData = Data.Instance.GetComponent<LastCustomizations> ().player2;
		character.Init (cData, avatarID, laneID);
		all.Add (character);
	}
	public void Move(float distance)
	{
		foreach (Character character in all)
			character.Move (distance);
	}
}
