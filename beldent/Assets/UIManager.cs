using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	static UIManager mInstance = null;
	public UICountdown uiCoutdown;
	public GameObject gameOver;

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
		Events.OnGameOver += OnGameOver;
		uiCoutdown.Init ();
		gameOver.SetActive (false);
	}
	void OnDestroy()
	{
		Events.OnGameOver -= OnGameOver;
	}
	void OnGameOver()
	{
		gameOver.SetActive (true);
	}

}
