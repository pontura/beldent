using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {
	public static System.Action<Character, int> OnCharacterChangeLane = delegate { };
	public static System.Action<Character> AddFollower = delegate { };
	public static System.Action OnGameOver = delegate { };
	public static System.Action<int> OnCrash = delegate { };
	public static System.Action<int> OnButtonClicked = delegate { };
	public static System.Action<int, int> OnJoystickAxisVertical = delegate { };
}
