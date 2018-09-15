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

    public bool trainGestures = true;
    public string newGestureName;

    public AppState appState;

    public delegate int PerformCalculation(int x, int y);


    private void Start()
    {
        // Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/vr-remix-gestures/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
        // Load user custom gestures
        //string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        //foreach (string filePath in filePaths)
            //trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
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


        if (trainGestures) {
            StoreGesture();
        } else {
            string gesture = RecogniseGesture();
            appState.ToggleState(gesture);
        }



        points.Clear();
        ink.enabled = false;
        ink.Clear();

    }

    void BuildPointsArray()
    {
        // Get positions from ink trail renderer
        Vector3[] trailRecorded = new Vector3[ink.positionCount];
        int positions = ink.GetPositions(trailRecorded);

        // Add PPDollar Point objects for each position on ink to points array
        foreach (Vector3 trail in trailRecorded)
        {
            points.Add(new Point(trail.x, -trail.y, 0));
        }
    }


    void StoreGesture() 
    {
        BuildPointsArray();
        string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName, DateTime.Now.ToFileTime());

        GestureIO.WriteGesture(points.ToArray(), newGestureName, fileName);
        Debug.Log(fileName);
        trainingSet.Add(new Gesture(points.ToArray(), newGestureName));
    }


    string RecogniseGesture() 
    {

        BuildPointsArray();
        // Create PDollar gesture and classify against existing gestures
        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        string resultString = gestureResult.GestureClass + " " + gestureResult.Score;
        Debug.Log(resultString);
        if (debugText) {
            debugText.text = resultString;
        }
        return gestureResult.Score > 0.8 ? gestureResult.GestureClass : "";
    }
}
