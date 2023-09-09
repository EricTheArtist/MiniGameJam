using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggmanAnimation : MonoBehaviour
{

    public Animator EggAnim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            EggAnim.SetBool("Running",true);
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            EggAnim.SetBool("Running", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EggAnim.SetTrigger("Jump");
        }
    }
}
