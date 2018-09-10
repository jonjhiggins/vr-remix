using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

using PDollarGestureRecognizer;

public class DrawInk : MonoBehaviour {
    private TrailRenderer ink;
    private List<Gesture> trainingSet = new List<Gesture>();
    private List<Point> points = new List<Point>();

    public Transform gestureOnScreenPrefab;
    public Text debugText;

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

    public void InkOn () 
    {
        ink.Clear();
        ink.enabled = true;
    }

    public void InkOff ()
    {
        Recognise();
        points.Clear();
        ink.enabled = false;
        ink.Clear();

    }

    void Recognise () 
    {
        // Get positions from ink trail renderer
        Vector3[] trailRecorded = new Vector3[ink.positionCount];
        int positions = ink.GetPositions(trailRecorded);

        // Add PPDollar Point objects for each position on ink to points array
        foreach (Vector3 trail in trailRecorded)
        {
            points.Add(new Point(trail.x, -trail.y, 0));
        }

        // Create PDollar gesture and classify against existing gestures
        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        string resultString = gestureResult.GestureClass + " " + gestureResult.Score;
        Debug.Log(resultString);
        if (debugText) {
            debugText.text = resultString;
        }
    }
}
