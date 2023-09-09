using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParemtControl : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        var platformMovement = other.collider.GetComponent<TopDownMovementController>();
        if (platformMovement != null)
        {
            platformMovement.SetParent(transform);
        }

    }

    private void OnCollisionExit(Collision other)
    {
        var platformMovement = other.collider.GetComponent<TopDownMovementController>();
        if (platformMovement != null)
        {
            Debug.Log("Exited Collider");
            platformMovement.ResetParent();
        }
    }
}