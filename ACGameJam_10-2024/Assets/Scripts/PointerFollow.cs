using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerFollow : MonoBehaviour
{
    public Vector3 mouseLoc;
    void Start()
    {
        
    }

    void Update()
    {
        mouseLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseLoc.x,mouseLoc.y,0);
    }
}
