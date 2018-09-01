using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DrawInk : MonoBehaviour {
    private TrailRenderer ink;

    void Awake () {
        // Start with ink disabled
        ink = gameObject.GetComponent<TrailRenderer>();
        ink.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        ink.enabled = Input.GetKey(KeyCode.Space);
        if (Input.GetKeyUp(KeyCode.Space)) {
            ink.Clear();
        } 
    }
}
