using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float speed;
    public Vector3 inkToControllerDistance;
    //public AnimationCurve curve;
    
    private void Awake()
    {
        // Set initial position in relation to controller
        transform.position = GetControllerTransformed(target.position);
    }

    // Transform controller position 
    private Vector3 GetControllerTransformed(Vector3 targetPosition) {
        return inkToControllerDistance + targetPosition; 
    }

    private void Update()
    {
        Vector3 controllerTransformed = GetControllerTransformed(target.position);
        // Lerp to controller (transformed)
        transform.position = Vector3.Lerp(transform.position, controllerTransformed, speed * Time.deltaTime);
    }
}
