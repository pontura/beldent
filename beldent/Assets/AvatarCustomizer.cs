using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarCustomizer : MonoBehaviour {
	
	public CustomizationData data;

	public SpriteRenderer[] piel;
	public SpriteRenderer[] gorra;
	public SpriteRenderer[] pantalon;
	public SpriteRenderer[] zapatos;
	public SpriteRenderer[] remera;
	public SpriteRenderer[] pelos;
	public SpriteRenderer[] barbas;

	public GameObject peloAsset;
	public GameObject gorraAsset;

	public void Init(CustomizationData data) {

		peloAsset.SetActive (true);
		gorraAsset.SetActive (true);

		this.data = data;
		if (data.pelo == "")
			peloAsset.SetActive (false);
		else if (data.gorra == "")
			gorraAsset.SetActive (false);

		//
		barbas[0].sprite = Resources.Load<Sprite>("clothes/img_barba_" +data.barba);
		gorra[0].sprite = Resources.Load<Sprite>("clothes/img_gorra_" +data.gorra+ "_a");

		Colorize(piel, data.color_piel);
		Colorize(pelos, data.color_pelos);
		Colorize(gorra, data.color_gorras);
		Colorize(zapatos, data.color_zapatos);
		Colorize(remera, data.color_remeras);
		Colorize(pantalon, data.color_pantalones);
		//pelos[0].sprite = Resources.Load<Sprite>("clothes/img_pelos_" +data.gorra+ "_a");
	}
	void Colorize(SpriteRenderer[] arr, Color color)
	{
		foreach (SpriteRenderer sr in arr)
			sr.color = color;
	}

}
