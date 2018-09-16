using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppState : MonoBehaviour
{

    bool triangle = false;
    bool square = false;
    bool circle = false;

    public Text debugText;

    public AudioSource triangleAudioTrack;
    public AudioSource squareAudioTrack;
    public AudioSource circleAudioTrack;

    public AudioSource confirmGesture;

    public Image triangleImage;
    public Image squareImage;
    public Image circleImage;

    public Animator triangleAnimator;
    public Animator squareAnimator;
    public Animator circleAnimator;

    public ParticleSystem inkExplode;

    Color inactiveColour = new Color(255, 255, 255, 0.5f);
    Color activeColour = new Color(255, 255, 255, 1);

    double startTick;
    double deltaDSP, oldDeltaDSP;

    AnimatorStateInfo triangleAnimatorStateInfo;
    AnimatorStateInfo squareAnimatorStateInfo;
    AnimatorStateInfo circleAnimatorStateInfo;

    public void ToggleState(string shape)
    {
        bool stateHasChanged = true;
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
            default:
                stateHasChanged = false;
                break;
        }
        // Output debug text
        debugText.text = "triangle " + triangle + " | square " + square + " | circle " + circle;
        if (stateHasChanged)
        {
            StateChanged(shape);
        }
    }

    private void Start()
    {
        PlayAudio();
        StateChanged("");
    }

    void PlayAudio()
    {
        startTick = AudioSettings.dspTime;
        triangleAudioTrack.PlayScheduled(startTick);
        circleAudioTrack.PlayScheduled(startTick);
        squareAudioTrack.PlayScheduled(startTick);
    }

    // @TODO replace with broadcasting an event
    void StateChanged(string shape)
    {
        // Has the user turned on a track via gesture?
        bool gestureOn = (shape == "triangle" && triangle || shape == "square" && square || shape == "circle" && circle);

        triangleAnimatorStateInfo = triangleAnimator.GetCurrentAnimatorStateInfo(0);
        circleAnimatorStateInfo = circleAnimator.GetCurrentAnimatorStateInfo(0);
        squareAnimatorStateInfo = squareAnimator.GetCurrentAnimatorStateInfo(0);

        triangleAudioTrack.volume = triangle ? 1 : 0;
        squareAudioTrack.volume = square ? 1 : 0;
        circleAudioTrack.volume = circle ? 1 : 0;

        triangleImage.color = triangle ? activeColour : inactiveColour;
        squareImage.color = square ? activeColour : inactiveColour;
        circleImage.color = circle ? activeColour : inactiveColour;

        triangleAnimator.SetBool("active", triangle);
        squareAnimator.SetBool("active", square);
        circleAnimator.SetBool("active", circle);

        if (gestureOn)
        {
            ConfirmGesture();
        }

    }

    void ConfirmGesture()
    {
        confirmGesture.Play();
        inkExplode.Play();
    }

    void Update()
    {
        oldDeltaDSP = deltaDSP;
        deltaDSP = AudioSettings.dspTime - startTick;

        // Change time for currently played state
        if (triangle)
        {
            triangleAnimator.PlayInFixedTime(0, 0, (float)(deltaDSP));
        }

        if (triangle)
        {
            circleAnimator.PlayInFixedTime(0, 0, (float)(deltaDSP));
        }

        if (square)
        {
            squareAnimator.PlayInFixedTime(0, 0, (float)(deltaDSP));
        }



    }
}
