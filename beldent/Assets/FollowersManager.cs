using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowersManager : MonoBehaviour {
	
	public List<Enemy> all;
	public Enemy enemy;
	Lanes lanes;
	float offset = 6;

	void Awake () {
		lanes = GetComponent<Lanes> ();
		Events.AddFollower += AddFollower;
		//Events.OnCharacterChangeLane += OnCharacterChangeLane;
		Events.OnGameStart += OnGameStart;
		Events.OnAvatarCatched += OnAvatarCatched;
	}
	void OnDestroy () {
		Events.AddFollower -= AddFollower;
		//Events.OnCharacterChangeLane -= OnCharacterChangeLane;
		Events.OnGameStart -= OnGameStart;
		Events.OnAvatarCatched -= OnAvatarCatched;
	}
	void OnAvatarCatched(Character character)
	{
		foreach (Enemy e in all) {
			if (e.characterToFollow == character)
				e.StopRunning ();
		}
	}
	void OnGameStart()
	{		
		foreach (Enemy e in all)
			e.StartRunning ();

		Loop ();
	}
	void Loop()
	{
		float dist;
		foreach (Enemy e in all) {
			if(e.characterToFollow.state != Character.states.DEAD && e.characterToFollow.state != Character.states.CRASH)
			StartCoroutine(e.ChangeLane () );
		}
		Invoke ("Loop", 0.75f);
	}
	void AddFollower(Character character)
	{
		Enemy newEnemy = Instantiate (enemy);
		all.Add (newEnemy);
		//newEnemy.laneID = character.laneID;
		newEnemy.distance = -offset;
		newEnemy.InitCharacter (Data.Instance.customizer.GetRandomData(), character, 0);

		Vector3 pos = character.transform.localPosition;
		pos.x = -offset;
		newEnemy.transform.localPosition = pos;

		print (pos);
		if (BoardManager.Instance.state == BoardManager.states.ACTIVE)
			newEnemy.Revive (pos);
	}
	public void Move(float distance)
	{
		foreach (Enemy e in all) {
			if (e.state == Enemy.states.HIT) {
				if (distance > e.transform.localPosition.x + 10) {
					Vector3 pos = e.transform.localPosition;
					pos.x = distance - offset;
					e.transform.localPosition = pos;
					e.Revive (e.characterToFollow.transform.localPosition);
				}
			}
			if (e.characterToFollow.state != Character.states.DEAD && e.characterToFollow.state != Character.states.CRASH) 
				e.Move (distance - offset, Time.deltaTime * 0.15f);
		}
	}
}
