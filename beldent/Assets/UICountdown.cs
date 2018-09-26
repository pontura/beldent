using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountdown : MonoBehaviour {

	public GameObject panel;
	int counter;
	public Text field;

	void Start () {
		panel.SetActive (false);
	}
	public void Init()
	{
		counter = 3;
		panel.SetActive (true);
		Loop ();
	}
	void Loop()
	{
		field.text = counter.ToString();

		if (counter == 0)
			Done ();
		counter--;
		Invoke ("Loop", 1);
	}
	void Done()
	{
		panel.SetActive (false);
		BoardManager.Instance.Init ();
	}

}
