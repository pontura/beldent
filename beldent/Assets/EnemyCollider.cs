using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour {

	Enemy enemy;
	void Start () {
		enemy = GetComponent<Enemy> ();
	}

	void OnTriggerEnter(Collider other)
	{
		Obstacle obstacle = other.GetComponent<Obstacle> ();
		if (obstacle != null) 
				enemy.Hit ();

	}
}
