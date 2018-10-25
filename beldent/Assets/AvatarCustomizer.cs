using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarCustomizer : MonoBehaviour {
	
	public CustomizationData data;

	public SpriteRenderer[] pelos_onlyColorChange;
	public SpriteRenderer[] piel;
	public SpriteRenderer[] gorra;
	public SpriteRenderer[] pantalon;
	public SpriteRenderer[] zapatos;
	public SpriteRenderer[] remera;
	public SpriteRenderer[] pelos;
	public SpriteRenderer[] barbas;
	public SpriteRenderer remeranotint;
	public SpriteRenderer pantalonnotint;

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

		if (data.barba == "") {
			barbas [0].gameObject.SetActive (false);
		} else {
			barbas [0].sprite = Resources.Load<Sprite> ("clothes/img_barba_" + data.barba);
			barbas [0].gameObject.SetActive (true);
		}

		gorra[0].sprite = Resources.Load<Sprite>("clothes/img_gorra_" +data.gorra+ "_a");

		pelos[0].sprite = Resources.Load<Sprite>("clothes/img_pelos_" +data.pelo+ "_b");
		pelos[1].sprite = Resources.Load<Sprite>("clothes/img_pelos_" +data.pelo+ "_a");


		remera[0].sprite = Resources.Load<Sprite>("clothes/img_remera_" +data.remera+ "_a");
		remera[1].sprite = Resources.Load<Sprite>("clothes/img_remera_" +data.remera+ "_b");
		remera[2].sprite = Resources.Load<Sprite>("clothes/img_remera_" +data.remera+ "_b");

		remeranotint.sprite = Resources.Load<Sprite>("clothes/img_remeranotint_" +data.remeranotint);
		pantalonnotint.sprite = Resources.Load<Sprite>("clothes/img_pantalonnotint_" +data.pantalonnotint);

		pantalon[0].sprite = Resources.Load<Sprite>("clothes/img_pantalon_" +data.pantalon+ "_b");
		pantalon[1].sprite = Resources.Load<Sprite>("clothes/img_pantalon_" +data.pantalon+ "_a");
		pantalon[2].sprite = Resources.Load<Sprite>("clothes/img_pantalon_" +data.pantalon+ "_a");



		Colorize(piel, data.color_piel);
		Colorize(pelos, data.color_pelos);
		Colorize(pelos_onlyColorChange, data.color_pelos);
		Colorize(gorra, data.color_gorras);
		Colorize(zapatos, data.color_zapatos);
		Colorize(remera, data.color_remeras);
		Colorize(pantalon, data.color_pantalones);
		Colorize(barbas, data.color_pelos);
		//pelos[0].sprite = Resources.Load<Sprite>("clothes/img_pelos_" +data.gorra+ "_a");
	}
	void Colorize(SpriteRenderer[] arr, Color color)
	{
		foreach (SpriteRenderer sr in arr)
			sr.color = color;
	}

}
