using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {
    public bool drawing = false;
    public Vector3 controllerPostition = new Vector3(0, 0, 0); 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(drawing + " | " + controllerPostition);
	}
}
