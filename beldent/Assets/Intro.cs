using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

	public AvatarCustomizer player1;
	public AvatarCustomizer player2;

	bool canClick;
	void Start () {
		Invoke ("Delayed1", 0.1f);
		Events.OnButtonClicked += OnButtonClicked;
		player1.gameObject.SetActive (false);
		player2.gameObject.SetActive (false);
	}
	void Delayed1()
	{
		LastCustomizations lastCustomizations = Data.Instance.GetComponent<LastCustomizations> ();
		Invoke ("Delayed2", 1);
		player1.gameObject.SetActive (true);
		player2.gameObject.SetActive (true);
		player1.Init (lastCustomizations.player1);
		player2.Init (lastCustomizations.player2);
		player1.GetComponent<Animator> ().Play ("enemy_win");
		player2.GetComponent<Animator> ().Play ("enemy_win");
	}
	void OnDestroy()
	{
		Events.OnButtonClicked -= OnButtonClicked;
	}
	void Delayed2()
	{
		canClick = true;
	}
	void OnButtonClicked(int a)
	{
		if(canClick)
			Data.Instance.LoadLevel ("Game");
	}
}
