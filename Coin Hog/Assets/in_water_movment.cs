using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class in_water_movment : MonoBehaviour {
    public Rigidbody2D body;
    float Drag = 0;
    //int yDrag = 0;
	// Use this for initialization
	void Start () {
        Drag = body.drag;
	}
	
	// Update is called once per frame
	void Update () {

        if (body.IsTouchingLayers(4))
        {
            body.drag = 10;
            print("water entered\n");

        }
        else 
        {
            body.drag = Drag;
        }
    }
}
