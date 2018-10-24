using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summary : MonoBehaviour {

	public Text scoreField;
	bool canClick;
	void Start () {
		Invoke ("Delayed", 1);
		Events.OnButtonClicked += OnButtonClicked;
		scoreField.text = "SCORE: " + Data.Instance.score;
	}
	void OnDestroy()
	{
		Events.OnButtonClicked -= OnButtonClicked;
	}
	void Delayed()
	{
		canClick = true;
	}
	void OnButtonClicked(int a)
	{
		if(canClick)
			Data.Instance.LoadLevel ("Intro");
	}
}
