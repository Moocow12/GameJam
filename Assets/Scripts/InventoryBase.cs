using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBase : MonoBehaviour {

    //LocalVariables
    RectTransform _rect;



    private Slot[] slots;
    public int numberOfSlots;


    public GameObject slotPrefab;



	// Use this for initialization
	void Start () {
        _rect = GetComponent<RectTransform>();
        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Camera.main.pixelWidth);
        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Camera.main.pixelHeight);
        slots = new Slot[numberOfSlots];
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize()
    {
        foreach(Slot s in slots)
        {
            GameObject obj = Instantiate(slotPrefab, transform);
            
        }

    }
}
