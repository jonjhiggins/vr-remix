using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    //Target
    public Transform target;
    public float speed;
    //public AnimationCurve curve;

    private void Update()
    {
        //Lerp to controller
        transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
    }
}
