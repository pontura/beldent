using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectsManager : MonoBehaviour {

	public List<SceneObject> sceneObjects_pooled;
	public List<SceneObject> sceneObjects_inScene;
	BoardManager boardManager;
	public Lanes lanes;
	public SceneObject obstacle1;
	public SceneObject obstacle2;
	public SceneObject enemy;
	public SceneObject grab1;
	public Transform container;

	void Start () {
		boardManager = GetComponent<BoardManager> ();
		Events.Pool += Pool;
		LoopToPool ();
	}
	void OnDestroy () {
		Events.Pool -= Pool;
	}
	void LoopToPool()
	{
		if (BoardManager.Instance.state == BoardManager.states.ACTIVE) 
		{
			int i = sceneObjects_inScene.Count;
			while (i > 0) {
				SceneObject so = sceneObjects_inScene [i - 1];
				if (so.transform.localPosition.x < boardManager.distance - 12) {
					Pool (so);
				}
				i--;
			}
		}
		Invoke ("LoopToPool", 2);
	}
	public void Add(SceneObjectData data, float totalDistance)
	{
		SceneObject so;
		so = GetSceneObject(data.type);

		if (so == null) {
			so = CreateNew (data.type);
			so.data = data;
		}

		sceneObjects_inScene.Add (so);
		Vector3 pos = lanes.GetCoordsByLane (data.laneID);
		pos.x = data.pos.x + totalDistance;
		so.transform.SetParent (container);
		so.transform.localPosition = pos;
		so.AddToLane (data.laneID);
		so.Init ();

		if(so.GetComponent<Enemy>())
			so.GetComponent<Enemy>().InitCharacter (Data.Instance.customizer.GetRandomData(), null, data.laneID);
	}
	public SceneObject GetSceneObject(SceneObjectData.types type)
	{
		foreach (SceneObject so in sceneObjects_pooled) {
			if (so.data.type == type) {
				so.gameObject.SetActive (true);
				sceneObjects_pooled.Remove (so);
				return so;
			}
		}
		return null;
	}
	void Pool (SceneObject so) {
		sceneObjects_inScene.Remove (so);
		sceneObjects_pooled.Add (so);
		so.OnPool ();
		so.gameObject.SetActive (false);

	}
	SceneObject CreateNew(SceneObjectData.types type)
	{
		SceneObject so_to_instantiate = obstacle1;

		switch (type) {
		case SceneObjectData.types.CHARACTER:
			so_to_instantiate = enemy;
			break;
		case SceneObjectData.types.OBSTACLE1:
			so_to_instantiate = obstacle1;
			break;
		case SceneObjectData.types.OBSTACLE2:
			so_to_instantiate = obstacle2;
			break;
		case SceneObjectData.types.GRAB:
			so_to_instantiate = grab1;
			break;
		}
		return Instantiate (so_to_instantiate);
	}
}
