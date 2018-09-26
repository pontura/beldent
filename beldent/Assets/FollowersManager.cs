using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowersManager : MonoBehaviour {
	
	public List<Enemy> all;
	public Enemy enemy;
	Lanes lanes;
	float offset = 5;

	void Start () {
		lanes = GetComponent<Lanes> ();
		Events.AddFollower += AddFollower;
		Events.OnCharacterChangeLane += OnCharacterChangeLane;
	}
	void OnDestroy () {
		Events.AddFollower -= AddFollower;
		Events.OnCharacterChangeLane -= OnCharacterChangeLane;
	}
	void OnCharacterChangeLane(Character character, int laneID)
	{
		float dist;
		foreach (Enemy e in all) {
			if (e.characterToFollow == character)
				StartCoroutine(e.ChangeLane (laneID) );
		}
	}
	void AddFollower(Character character)
	{
		Enemy newEnemy = Instantiate (enemy);
		all.Add (newEnemy);
		newEnemy.laneID = character.laneID;
		newEnemy.Init (character, character.laneID);
	}
	public void Move(float distance)
	{
		foreach (Enemy e in all)
			e.Move (distance-offset);
	}
}
