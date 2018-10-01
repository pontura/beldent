using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizer : MonoBehaviour {

	public bool Reload;
	public Color[] color_pelos;
	public Color[] color_remeras;
	public Color[] color_piel;
	public Color[] color_zapatos;
	public Color[] color_gorras;
	public Color[] color_pantalones;

	[HideInInspector]
	public List<string> all;
	public List<string> gorras;
	public List<string> remeras;
	public List<string> remerasnotint;
	public List<string> pelos;
	public List<string> barbas;
	public List<string> pantalones;
	public List<string> pantalonnotint;

	void Awake () {
		
		if (!Reload)
			return;

		all.Clear ();
		gorras.Clear ();
		remeras.Clear ();
		pelos.Clear ();
		barbas.Clear ();
		remerasnotint.Clear ();
		pantalones.Clear ();
		pantalonnotint.Clear ();

		foreach (string name in System.IO.Directory.GetFiles(@"Assets\Resources\clothes", "*.png"))
		{
			string[] textSplit = name.Split(@"\"[0]);
			string[] textSplit2 = textSplit[3].Split("."[0]);
			string fileName = textSplit2 [0];
			string[] fileNameArr = fileName.Split("_"[0]);
			string part = fileNameArr[1];

			switch (part) {
			case "gorra":
				AddToArray( gorras, fileNameArr[2]);
				break;
			case "remera":
				AddToArray( remeras, fileNameArr[2]);
				break;
			case "pelos":
				AddToArray( pelos, fileNameArr[2]);
				break;
			case "barba":
				AddToArray( barbas, fileNameArr[2]);
				break;
			case "remeranotint":
				AddToArray( remerasnotint, fileNameArr[2]);
				break;
			case "pantalon":
				AddToArray( pantalones, fileNameArr[2]);
				break;
			case "pantalonnotint":
				AddToArray( pantalonnotint, fileNameArr[2]);
				break;
			}
			all.Add(fileName);
		}
	}
	public CustomizationData GetRandomData()
	{
		CustomizationData data = new CustomizationData ();
		if (Random.Range (0, 10) < 4) {
			data.remeranotint = "";
		}else{
			data.remeranotint = GetRandomFromArray(remerasnotint);
		}
		if (Random.Range (0, 10) < 4) {
			data.pantalonnotint = "";
		}else{
			data.pantalonnotint = GetRandomFromArray(pantalonnotint);
		}
		if (Random.Range (0, 10) < 5) {
			data.barba = "";
		}else{
			data.barba = GetRandomFromArray(barbas);
		}
		if (Random.Range (0, 10) < 5) {
			data.gorra = "";
			data.pelo = GetRandomFromArray(pelos);
		} else {
			data.pelo = "";
			data.gorra = GetRandomFromArray(gorras);
		}
		data.color_pelos = color_pelos[Random.Range (0,color_pelos.Length)];
		data.color_remeras = color_remeras[Random.Range (0,color_remeras.Length)];
		data.color_zapatos = color_zapatos[Random.Range (0,color_zapatos.Length)];
		data.color_piel = color_piel[Random.Range (0,color_piel.Length)];
		data.color_gorras = color_gorras[Random.Range (0,color_gorras.Length)];
		data.color_pantalones = color_pantalones[Random.Range (0,color_pantalones.Length)];

		data.remera = GetRandomFromArray(remeras);
		data.pantalon = GetRandomFromArray(pantalones);
		return data;
	}
	public void AddToArray(List<string> arr, string value)
	{
		foreach(string s in arr)
			if(s == value)
				return;
		arr.Add(value);
	}
	public string GetRandomFromArray(List<string> arr)
	{
		return arr[Random.Range(0,arr.Count)];
	}
}
