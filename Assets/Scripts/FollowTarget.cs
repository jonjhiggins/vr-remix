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
        transform.position = GetControllerTransformed(target);
    }

    // Transform controller position 
    private Vector3 GetControllerTransformed(Transform controllerTarget) {
        Vector3 targetPosition = target.position;
        target.localPosition = inkToControllerDistance;
        string debugString = target.rotation.x.ToString("0.0") + ' ' + target.rotation.y.ToString("0.0") + ' ' + target.rotation.z.ToString("0.0");
        debugText.text = debugString;
        return targetPosition;
    }

    private void Update()
    {
        Vector3 controllerTransformed = GetControllerTransformed(target);
        // Lerp to controller (transformed)
        transform.position = Vector3.Lerp(transform.position, controllerTransformed, speed * Time.deltaTime);
    }
}
