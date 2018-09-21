using UnityEngine;
using System.Collections;

public class Scrolleable : MonoBehaviour {

    private float distance;
    public float speed;
    public int distanceToRepeat;

    public void Move(float _x)
    {
        _x /= speed;
        distance += _x;
        Vector3 pos = transform.localPosition;
        pos.x -= _x;
        if (pos.x < -distanceToRepeat) pos.x = 0;
        transform.localPosition = pos;
    }
}
