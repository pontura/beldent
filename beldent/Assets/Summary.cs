using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summary : MonoBehaviour {


	void Start () {
		Invoke ("Timeout", 2);
	}
	void Timeout()
	{
		Data.Instance.LoadLevel ("Game");
	}
}
