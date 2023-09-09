using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceState : MonoBehaviour
{
    public PhysicMaterial noBounce;
    public PhysicMaterial bounce;

    public GameObject[] BounceObj;

    CountDownTimer timer;
    [SerializeField] GameObject GameManager;

    // Start is called before the first frame update
    void Start()
    {
        BounceObj= GameObject.FindGameObjectsWithTag("Bouncer");
        timer = GameManager.GetComponent<CountDownTimer>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.fullTime < 100)
        {
            SwitchBounce();
        }
    }

    void SwitchBounce()
    {
        foreach (GameObject obj in BounceObj)
        {
            obj.GetComponent<Collider>().material = bounce;
        }
        
    }
}