using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreOn : MonoBehaviour {

	public Text field;

	public void Init(int value)
	{
		StopAllCoroutines ();
		field.text = "+"+value.ToString ();
		gameObject.SetActive (true);
		StartCoroutine (doit ());
	}
	IEnumerator doit()
	{
		gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		gameObject.SetActive (false);
	}
}
