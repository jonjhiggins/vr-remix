using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using PDollarGestureRecognizer;

public class DrawInk : MonoBehaviour {
    private TrailRenderer ink;
    // public Camera screenshotCamera;
    private List<Gesture> trainingSet = new List<Gesture>();
    private List<Point> points = new List<Point>();

    public Transform gestureOnScreenPrefab;
    private int strokeId = -1;
    private int vertexCount = 0;

    private bool recognized;

    private void Start()
    {
        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
        //Load user custom gestures
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
    }


    void Awake () {
        // Start with ink disabled
        ink = gameObject.GetComponent<TrailRenderer>();
        ink.enabled = false;
    }

    private void Update()
    {
        if (!ink.enabled) {
            return;
        }

        if (recognized)
        {

            recognized = false;
            strokeId = -1;
            points.Clear();
            ink.Clear();
        }

        ++strokeId;

        Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;

        vertexCount = 0;
    }

    public void InkOn () 
    {
        ink.Clear();
        ink.enabled = true;
    }

    public void InkOff ()
    {
        //if (screenshotCamera) {
        //    screenshotCamera.GetComponent<ScreenRecorder>().CaptureScreenshot();
        //}
        // ink.enabled = false;
        Vector3[] trailRecorded = new Vector3[ink.positionCount];

        int positions = ink.GetPositions(trailRecorded);

        foreach (Vector3 trail in trailRecorded)
            Debug.Log(trail);

    }

    void Recognise () 
    {
        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        Debug.Log(gestureResult.GestureClass + " " + gestureResult.Score);
    }
}
