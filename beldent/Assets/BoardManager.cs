using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public float distance;

	public Scrolleable[] Zone1Objects;
	public AreasManager areasManager;
	public GameCamera cam;
	public states state;
	public enum states
	{
		IDLE,
		ACTIVE,
		INACTIVE,
		GAMEOVER
	}
	public float realSpeed = 0;
	public float speed;

	public void Init()
	{

	}
	void Update()
	{
		if (state == states.INACTIVE || state == states.GAMEOVER)
		{
			return;
		}
		if (realSpeed < speed)
			realSpeed += 0.1f;
//		else if (realSpeed > speed)
//			realSpeed -= 0.04f;


		if (state == states.ACTIVE)
		{
			float _speed = realSpeed*Time.smoothDeltaTime;
			distance += _speed;

			foreach (Scrolleable scrolleable in Zone1Objects)
				scrolleable.Move(_speed);

		}
		areasManager.Check (distance);
		cam.Move (distance);
	}
}
