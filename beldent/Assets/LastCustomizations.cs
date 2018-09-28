using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCustomizations : MonoBehaviour {

	public CustomizationData player1;
	public CustomizationData player2;

	void Start () {
		Customizer c = GetComponent<Customizer> ();
		player1 = c.GetRandomData ();
		player2 = c.GetRandomData ();
	}
	int playerID;
	public void SaveNewCustomization(CustomizationData data)
	{
		playerID++;
		if (playerID > 2)
			playerID = 1;
		
		if (playerID == 1)
			player1 = data;
		else
			player2 = data;
	}
}
