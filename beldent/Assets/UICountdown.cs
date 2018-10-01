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
		panel.SetActive (true);
		field.text = counter.ToString();

		if (counter == 0) {
			Done ();
			return;
		}
		Events.OnSoundFX ("countdown");
		counter--;
		Invoke ("Loop2", 0.8f);
	}
	void Loop2()
	{
		panel.SetActive (false);
		Invoke ("Loop", 0.1f);
	}
	void Done()
	{
		panel.SetActive (false);
		BoardManager.Instance.Init ();
	}

}
