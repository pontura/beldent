using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour {

	public Camera cam;
	public Text field;

	int score;

	public ScoreOn player1Score;
	public ScoreOn player2Score;

	void Start () {
		player1Score.gameObject.SetActive (false);
		player2Score.gameObject.SetActive (false);
		Events.OnScore += OnScore;
		Events.OnGameOver += OnGameOver;
	}
	void OnDestroy()
	{
		Events.OnScore -= OnScore;
		Events.OnGameOver -= OnGameOver;
	}
	void OnGameOver()
	{
		Data.Instance.score = score;
	}
	void OnScore (Character character, int value) {
		
		score += value;
		field.text = score.ToString ();

		ScoreOn scoreOn = player2Score;

		if (character.id == 1) 
			scoreOn = player1Score;

		scoreOn.gameObject.SetActive (false);
		
		Vector2 pos = cam.WorldToScreenPoint (character.transform.position);
		scoreOn.transform.position = pos;
		scoreOn.Init (value);
		scoreOn.gameObject.SetActive (true);
	}
}
