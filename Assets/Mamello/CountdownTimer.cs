using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float countdownTime = 30.0f;
    [SerializeField] private Collider objectCollider;
    [SerializeField] private PhysicMaterial currentMaterial;

    private PhysicMaterial originalMaterial;
    private PhysicMaterial bouncyMaterial;

    void Start()
    {
        // Get the original physics material
        originalMaterial = objectCollider.material;

      
        bouncyMaterial = new PhysicMaterial();
        bouncyMaterial.bounciness = 1;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.W)) 
        {
            objectCollider.material = bouncyMaterial; // Change the GameObject's physics material to the modified one
            Debug.Log(objectCollider.material);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            objectCollider.material = originalMaterial;
            Debug.Log(objectCollider.material);
        }
    }
}
