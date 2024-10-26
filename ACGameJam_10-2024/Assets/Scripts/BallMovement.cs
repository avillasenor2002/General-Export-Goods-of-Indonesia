using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class BallMovement : MonoBehaviour
{
    public float impulseForce;
    public float offsetMag;

    public bool isMoving;
    public bool clickedOn;

    public Rigidbody2D playerRB;

    public Vector2 mousePoint;
    public Vector2 screenPoint;

    public Vector3 thisPoint;
    public Vector3 targetPoint;
    public Vector3 offsetAmount;

    public LineRenderer targetLine;

    public GameObject tracker;


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        targetLine = GetComponent<LineRenderer>();
    }

    //the player must be clicked on to activate the movement script
    public void OnMouseDown()
    {
        clickedOn = true;
    }

    void Update()
    {
        //records mouse position when M1 is held down, points player at mouse position
        if (Input.GetMouseButton(0))
        {
            //if the player was clicked on, executes code; otherwise it does nothing
            if (clickedOn)
            {
                //shows the line between mouse cursor and player
                targetLine.enabled = true;

                //sets up all the points necessary to make the code work
                screenPoint = Input.mousePosition;
                mousePoint = Camera.main.ScreenToWorldPoint(screenPoint);
                thisPoint = transform.position;

                //weird math shit to make the lookangle work
                targetPoint.z = 0;
                mousePoint.x = mousePoint.x - thisPoint.x;
                mousePoint.y = mousePoint.y - thisPoint.y;

                //looks at the mouse position
                float lookAngle = Mathf.Atan2(mousePoint.x, mousePoint.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -lookAngle));

                //draws the line between targeter and player
                targetLine.SetPosition(0, transform.position);
                targetLine.SetPosition(1, tracker.transform.position);

                //takes in the magnitude of distance between the cursor and the player
                offsetAmount = transform.position - tracker.transform.position;
                offsetMag = offsetAmount.magnitude;

                //since the magnitude is used to determine ball speed, clamps the values so we don't get insane speeds 
                if (offsetMag > 5)
                {
                    offsetMag = 5;
                }
                else if (offsetMag < 0.1f)
                {
                    offsetMag = 1;
                }
            }
        }
        //adds force onto player in the opposite direction it's looking
        if (Input.GetMouseButtonUp(0))
        {
            //resets everything to normal, and adds a force dependent on magnitude of distance between the player and cursor
            clickedOn = false;
            targetLine.enabled = false;
            playerRB.AddForce(transform.up*(offsetMag*-3), ForceMode2D.Impulse);
        }
    }
}
