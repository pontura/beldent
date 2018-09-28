using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summary : MonoBehaviour {

	bool canClick;
	void Start () {
		Invoke ("Delayed", 1);
		Events.OnButtonClicked += OnButtonClicked;
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
