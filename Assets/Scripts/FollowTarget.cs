using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float speed;
    public Vector3 inkToControllerDistance;
    public Text debugText;
    //public AnimationCurve curve;

    private void Awake()
    {
        // Set initial position in relation to controller
        transform.position = GetControllerTransformed(target.position);
    }

    // Transform controller position 
    private Vector3 GetControllerTransformed(Vector3 targetPosition) {
        string debugString = targetPosition.x.ToString("0.0") + ' ' + targetPosition.y.ToString("0.0") + ' ' + targetPosition.z.ToString("0.0");
        debugText.text = debugString;
        // return inkToControllerDistance + targetPosition; 
        Vector3 newPosition = new Vector3(targetPosition.x, targetPosition.y, 1.7f);
        Debug.Log(targetPosition + " | " + newPosition);
        return newPosition;
    }

    private void Update()
    {
        Vector3 controllerTransformed = GetControllerTransformed(target.position);
        // Lerp to controller (transformed)
        transform.position = Vector3.Lerp(transform.position, controllerTransformed, speed * Time.deltaTime);
    }
}
