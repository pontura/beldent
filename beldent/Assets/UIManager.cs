using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	static UIManager mInstance = null;
	public UICountdown uiCoutdown;

	public static UIManager Instance
	{
		get
		{
			return mInstance;
		}
	}
	void Awake()
	{
		if (!mInstance)
			mInstance = this;
	}
	void Start()
	{
		uiCoutdown.Init ();
	}

}
