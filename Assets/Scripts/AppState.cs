using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppState : MonoBehaviour {

    bool triangle = false;
    bool square = false;
    bool circle = false;

    public Text debugText;

    public AudioSource triangleAudioTrack;
    public AudioSource squareAudioTrack;
    public AudioSource circleAudioTrack;

    public Image triangleImage;
    public Image squareImage;
    public Image circleImage;

    Color inactiveColour = new Color(255, 255, 255, 0.5f);
    Color activeColour = new Color(255, 255, 255, 1);

    public void ToggleState(string shape)
    {
        // Set state
        // @TODO use a dictionary instead?
        switch (shape)
        {
            case "triangle":
                triangle = !triangle;
                break;
            case "square":
                square = !square;
                break;
            case "circle":
                circle = !circle;
                break;
        }
        // Output debug text
        debugText.text = "triangle " + triangle + " | square " + square + " | circle " + circle;
        StateChanged();
    }

    private void Start()
    {
        StateChanged();
    }

    // @TODO replace with broadcasting an event
    void StateChanged()
    {
        triangleAudioTrack.volume = triangle ? 1 : 0;
        squareAudioTrack.volume = square ? 1 : 0;
        circleAudioTrack.volume = circle ? 1 : 0;

        triangleImage.color = triangle ? activeColour : inactiveColour;
        squareImage.color = square ? activeColour : inactiveColour;
        circleImage.color = circle ? activeColour : inactiveColour;
    }
}
