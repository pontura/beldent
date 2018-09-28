using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

	void Start () {
		Invoke ("Delayed", 1);
	}
	void Delayed()
	{
		Data.Instance.LoadLevel ("Game");
	}
}
