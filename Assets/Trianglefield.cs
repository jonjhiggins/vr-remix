using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trianglefield : MonoBehaviour
{
    // Use this for initialization

    public GameObject[] triangleTargets;

    void Start()
    {
        Invoke("ChangeDirection", 0.45F);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * 0.85F);
    }

    void ChangeDirection()
    {
        // transform.rotation = transform.rotation * Quaternion.Euler(135,135,135);
        Invoke("ChangeDirection", 0.45F);

        transform.LookAt(triangleTargets[Random.Range(0, triangleTargets.Length)].transform);
    }
}
