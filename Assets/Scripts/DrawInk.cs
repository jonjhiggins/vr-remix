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

    public void InkOn () 
    {
        ink.Clear();
        ink.enabled = true;
    }

    public void InkOff ()
    {
        ink.enabled = false;
    }
}
