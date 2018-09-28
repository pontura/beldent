using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AreasManager : MonoBehaviour {

	public List<Area> areas;

	SceneObjectsManager sceneObjectsManager;



	public Lanes lanes;
	public float totalDistance;
	public float screenSize = 20;
	int id;

	void Awake()
	{
		sceneObjectsManager = GetComponent<SceneObjectsManager> ();
		GameObject[] thisAreaSets = Resources.LoadAll<GameObject>("areas");
		foreach (GameObject go in thisAreaSets)
		{
			Area area = go.GetComponent<Area>() as Area;
			areas.Add(area);
		}
		RandomizeAreaSetsByPriority();
	}
	public void Check(float distance)
	{
		if (distance > totalDistance - screenSize)
			Add ();
	}
	public void Add() {
		if (id >= areas.Count - 1)
			id -= 5;
		Area newArea = areas[id];
		foreach (SceneObjectData data in newArea.GetObjects()) {
			sceneObjectsManager.Add (data, totalDistance);
		}
		totalDistance += newArea.length;

		if (Random.Range (0, 10) > 3)
			id++;

	}

	public void RandomizeAreaSetsByPriority()
	{
		areas = Randomize(areas);
		areas = areas.OrderBy(x => x.difficulty).ToList();
	}
	private List<Area> Randomize(List<Area> toRandom)
	{
		for (int i = 0; i < toRandom.Count; i++)
		{
			Area temp = toRandom[i];
			int randomIndex = Random.Range(i, toRandom.Count);
			toRandom[i] = toRandom[randomIndex];
			toRandom[randomIndex] = temp;
		}
		return toRandom;
	}
}
