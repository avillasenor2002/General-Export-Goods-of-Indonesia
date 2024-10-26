using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class BallMovement : MonoBehaviour
{
    //movement script variables
    public float impulseForce;
    public float offsetMag;

    public bool isMoving;
    public bool clickedOn;

    public Rigidbody2D playerRB;

    public Vector2 mousePoint;
    public Vector2 screenPoint;
    public Vector2 mousePosition;

    public Vector3 thisPoint;
    public Vector3 targetPoint;
    public Vector3 offsetAmount;

    public LineRenderer targetLine;

    public GameObject tracker;

    //player stats variables
    public int playerHealth;
    public int enemyHealthUpdate;

    public Vector2 playerSize;

    public bool playerGrowing;

    public AlexScreenShake screenShake;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        targetLine = GetComponent<LineRenderer>();
        targetLine.enabled = false;
    }

    //the player must be clicked on to activate the movement script
    public void OnMouseDown()
    {
        clickedOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerSize = transform.localScale;
        //records mouse position when M1 is held down, points player at mouse position
        if (Input.GetMouseButton(0))
        {
            //if the player was clicked on, executes code; otherwise it does nothing
            if (clickedOn == true)
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

                //takes in the magnitude of distance between the cursor and the player
                offsetAmount = transform.position - tracker.transform.position;
                offsetMag = offsetAmount.magnitude;

                //since the magnitude is used to determine ball speed, clamps the values so we don't get insane speeds 
                if (offsetMag > 5)
                {
                    offsetMag = 5;
                    targetLine.SetPosition(0, transform.position);
                    targetLine.SetPosition(1, tracker.transform.position);
                }
                else if (offsetMag < 0.1f)
                {
                    offsetMag = 1;
                }

                //draws the line between targeter and player
                if (offsetMag < 5)
                {
                    targetLine.SetPosition(0, transform.position);
                    targetLine.SetPosition(1, tracker.transform.position);
                }
            }
        }
        //adds force onto player in the opposite direction it's looking
        if (Input.GetMouseButtonUp(0))
        {
            if (clickedOn == true)
            {
                //resets everything to normal, and adds a force dependent on magnitude of distance between the player and cursor
                clickedOn = false;
                targetLine.enabled = false;
                playerRB.AddForce(transform.up*(offsetMag*-impulseForce), ForceMode2D.Impulse);
            }
        }

        if (playerGrowing == true)
        {
            GrowPlayer(enemyHealthUpdate);
        }
    }

    public void OnTriggerEnter2D(Collider2D trig)
    {
        EnemyScript enScript = trig.gameObject.GetComponentInParent< EnemyScript>();
        if (enScript != null)
        {
            if (enScript.currentHealth < playerHealth)
            {
                if (screenShake != null)
                {
                    screenShake.IsShaking();
                }
                Destroy(enScript.gameObject);
                //GrowPlayer(enScript.currentHealth);
                playerGrowing = true;
                enemyHealthUpdate = enScript.currentHealth;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        EnemyScript enScript = col.gameObject.GetComponent<EnemyScript>();
        if (enScript != null)
        {
            if (enScript.currentHealth == playerHealth)
            {
                if (screenShake != null)
                {
                    screenShake.IsShaking();
                }
                playerHealth = playerHealth + enScript.currentHealth;
                enemyHealthUpdate = enScript.currentHealth;
                //GrowPlayer(enScript.currentHealth);
                playerGrowing = true;
                enScript.isLaunched = true;
                StartCoroutine(BelatedDeath(col.gameObject));
            }
            else if (enScript.currentHealth > playerHealth)
            {
                if (screenShake != null)
                {
                    screenShake.IsShaking();
                }
                Destroy(this.gameObject);
                col.rigidbody.drag = 1000;
                col.rigidbody.angularDrag = 1000;
            }
        }
    }

    IEnumerator BelatedDeath(GameObject enemy)
    {
        yield return new WaitForSeconds(2);
        Destroy(enemy);
    }

    public void GrowPlayer(int enemyHealth)
    {
        if ((Mathf.Round(playerHealth * 100))/100 <  enemyHealth*4)
        {
            transform.localScale = new Vector2(Mathf.Lerp(transform.localScale.x, enemyHealth * 4, 0.1f), Mathf.Lerp(transform.localScale.y, enemyHealth * 4, 0.1f));
        }
        else
        {
            transform.localScale = new Vector2(enemyHealth*4,enemyHealth*4);
            playerGrowing = false;
        }
    }
}
