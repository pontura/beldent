using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public float distance;

	public Scrolleable[] Zone1Objects;
	public CharactersManager charactersManager;
	public FollowersManager followersManager;
	public AreasManager areasManager;
	public Lanes lanes;
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
	static BoardManager mInstance = null;
	bool lastAvatarReached;
	public float acceleration = 0;
	public static BoardManager Instance
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
		Events.OnAvatarCatched += OnAvatarCatched;
	}
	void OnDestroy()
	{
		Events.OnGameOver -= OnGameOver;
		Events.OnAvatarCatched -= OnAvatarCatched;
	}
	void OnAvatarCatched(Character character)
	{
		if(state == states.GAMEOVER)
		lastAvatarReached = true;
	}
	public void Init()
	{
		lastAvatarReached = false;
		state = states.ACTIVE;
		Events.OnGameStart ();
	}
	void OnGameOver()
	{
		state = states.GAMEOVER;
		Invoke ("TimeOut", 4);
	}
	void TimeOut()
	{
		Data.Instance.LoadLevel ("Summary");
	}
	void Update()
	{
		if (state == states.IDLE || lastAvatarReached)
		{
			return;
		}
		acceleration += Time.deltaTime * 0.0005f;

		if (realSpeed < speed)
			realSpeed += 0.1f;

		float _speed = (realSpeed*Time.smoothDeltaTime);
		distance += _speed+acceleration;

		followersManager.Move (distance);

		if (state == states.GAMEOVER)
			return;

		foreach (Scrolleable scrolleable in Zone1Objects)
			scrolleable.Move(_speed);
	
		areasManager.Check (distance);
		cam.Move (distance);
		charactersManager.Move (distance);
	}

}
