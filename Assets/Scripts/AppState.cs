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

    public AudioSource confirmGestureAudio;

    public Image[] triangleImages;
    public Image[] squareImages;
    public Image[] circleImages;

    public Animator[] triangleAnimators;
    public Animator[] squareAnimators;
    public Animator[] circleAnimators;

    public Material triangleParticle;
    public Material squareParticle;
    public Material circleParticle;

    public AudioClip triangleConfirmAudio;
    public AudioClip squareConfirmAudio;
    public AudioClip circleConfirmAudio;

    public ParticleSystem inkExplode;

    public ParticleSystem starfield;
    public ParticleSystem warpfield;

    public TrailRenderer trianglefield;

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
        // bool gestureOn = (shape == "triangle" && triangle || shape == "square" && square || shape == "circle" && circle);

        triangleAnimatorStateInfo = triangleAnimators[0].GetCurrentAnimatorStateInfo(0);
        circleAnimatorStateInfo = circleAnimators[0].GetCurrentAnimatorStateInfo(0);
        squareAnimatorStateInfo = squareAnimators[0].GetCurrentAnimatorStateInfo(0);

        triangleAudioTrack.volume = triangle ? 1 : 0;
        squareAudioTrack.volume = square ? 1 : 0;
        circleAudioTrack.volume = circle ? 1 : 0;


        foreach (Image triangleImage in triangleImages)
        {
            triangleImage.color = triangle ? activeColour : inactiveColour;
        }

        foreach (Image squareImage in squareImages)
        {
            squareImage.color = square ? activeColour : inactiveColour;
        }

        foreach (Image circleImage in circleImages)
        {
            circleImage.color = circle ? activeColour : inactiveColour;
        }

        foreach (Animator triangleAnimator in triangleAnimators)
        {
            triangleAnimator.SetBool("active", triangle);
        }

        foreach (Animator squareAnimator in squareAnimators)
        {
            squareAnimator.SetBool("active", square);
        }

        foreach (Animator circleAnimator in circleAnimators)
        {
            circleAnimator.SetBool("active", circle);
        }



        
        
        

        PlayVisualisations(shape);

        if (shape != "")
        {
            ConfirmGesture(shape);
        }

    }

    void PlayVisualisations(string shape)
    {
        switch (shape)
        {
            case "triangle":
                trianglefield.enabled = triangle;
                break;
            case "square":
                if (square)
                {
                    warpfield.Play();
                }
                else
                {
                    warpfield.Stop();
                }
                break;
            case "circle":
                if (circle)
                {
                    starfield.Play();
                }
                else
                {
                    starfield.Stop();
                }

                break;
        }

    }

    void ConfirmGesture(string shape)
    {
        // Set particle texture to shape detected
        Renderer renderer = inkExplode.GetComponent<Renderer>();
        switch (shape)
        {
            case "triangle":
                renderer.material = triangleParticle;
                confirmGestureAudio.clip = triangleConfirmAudio;
                break;
            case "square":
                renderer.material = squareParticle;
                confirmGestureAudio.clip = squareConfirmAudio;
                break;
            case "circle":
                renderer.material = circleParticle;
                confirmGestureAudio.clip = circleConfirmAudio;
                break;
        }
        confirmGestureAudio.Play();
        inkExplode.Play();
    }

    void Update()
    {
        oldDeltaDSP = deltaDSP;
        deltaDSP = AudioSettings.dspTime - startTick;

        DebugKeyboardListener();

        foreach (Animator triangleAnimator in triangleAnimators)
        {
            triangleAnimator.PlayInFixedTime(0, 0, (float)(deltaDSP));
        }

        foreach (Animator squareAnimator in squareAnimators)
        {
            squareAnimator.PlayInFixedTime(0, 0, (float)(deltaDSP));
        }

        foreach (Animator circleAnimator in circleAnimators)
        {
            circleAnimator.PlayInFixedTime(0, 0, (float)(deltaDSP));
        }



    }

    /* Keyboard keypresses to toggle shape state when in Unity
    1 = circle
    2 = triangle
    3 = square
     */
    void DebugKeyboardListener()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleState("circle");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleState("triangle");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToggleState("square");
        }

    }
}
