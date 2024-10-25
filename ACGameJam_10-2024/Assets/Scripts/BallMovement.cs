using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Vector2 mousePosition;
    public float impulseForce;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //inputs mouse position
        }
        if (Input.GetMouseButtonUp(0))
        {
            //points towards mouse position, inputs inverse force
        }
    }
}
