using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizer : MonoBehaviour {

	public List<string> all;
	public List<string> gorras;
	public List<string> remeras;

	void Start () {
		foreach (string name in System.IO.Directory.GetFiles(@"Assets\Resources\clothes", "*.png"))
		{
			string[] textSplit = name.Split(@"\"[0]);
			string[] textSplit2 = textSplit[3].Split("."[0]);
			string fileName = textSplit2 [0];
			string[] fileNameArr = fileName.Split("_"[0]);
			string part = fileNameArr[1];
			switch (part) {
			case "gorra":
				gorras.Add(fileName);
				break;
			case "remera":
				remeras.Add(fileName);
				break;
			}
			all.Add(fileName);
		}
	}
}
