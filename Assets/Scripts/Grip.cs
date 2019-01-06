using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    private float distance = 10f;
    private Launcher parent;

	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<Launcher>();
	}

    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        parent.SetGripped(true);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }
}
