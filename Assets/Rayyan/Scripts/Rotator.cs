using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotatorSpeed;

    private void Update()
    {
        Rotating();
    }

    void Rotating()
    {
        transform.Rotate(Vector3.up* (rotatorSpeed*Time.deltaTime));
    }
}
