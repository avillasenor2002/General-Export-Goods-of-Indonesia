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
    public bool isMyTurn;
    public bool enemyTurn= false;
    public bool clickedOn;

    public Rigidbody2D playerRB;

    public Vector2 mousePoint;
    public Vector2 screenPoint;
    public Vector2 mousePosition;

    public Vector3 thisPoint;
    public Vector3 targetPoint;
    public Vector3 offsetAmount;

    public LineRenderer targetLine;

    public ParticleSystem deathParticles;

    public GameObject tracker;

    //player stats variables
    public int playerHealth;
    public int playerHealthUpdate;
    public int enemyHealthUpdate;
    public int playerHealthAdded;
    public int turnEnd;
    public int turnAmount;

    public Vector2 playerSize;

    public bool playerGrowing;

    public AlexScreenShake screenShake; 

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        targetLine = GetComponent<LineRenderer>();
        targetLine.enabled = false;
        turnEnd = 0;
        isMyTurn = true;
    }

    //the player must be clicked on to activate the movement script
    public void OnMouseDown()
    {
        if (isMoving == false)
        {
            clickedOn = true;
        }
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
                    Vector2 newPos = (transform.position - tracker.transform.position)*-1;
                    targetLine.SetPosition(0, transform.position);
                    targetLine.SetPosition(1, transform.position + Vector3.ClampMagnitude(newPos, offsetMag));
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
                isMoving = true;
                targetLine.enabled = false;
                playerRB.AddForce(transform.up*(offsetMag*-impulseForce), ForceMode2D.Impulse);
                turnEnd = 0;
                isMyTurn = false;
            }
        }

        if (playerRB.velocity.magnitude <= 0.8f)
        {
            if (playerRB.velocity.magnitude >= 0)
            {
                playerRB.velocity = new Vector2 (Mathf.Lerp(playerRB.velocity.x, 0, 1f), Mathf.Lerp(playerRB.velocity.y, 0, 1f));
            }
        }
        if ((playerRB.velocity.magnitude == 0)&& isMyTurn==false)
        {
            isMoving = false;
            isMyTurn = true;
            turnAmount++;
        }

        if (playerGrowing == true)
        {
            GrowPlayer(playerHealthUpdate);
        }
    }


    public void OnCollisionEnter2D(Collision2D col)
    {
        EnemyScript enScript = col.gameObject.GetComponent<EnemyScript>();
        if (enScript != null)
        {
            //script for if an enemy is equal to the player
            if (enScript.currentHealth <= playerHealth)
            {
                if (enScript.isDying == false)
                {
                    enScript.isDying= true;
                    if (screenShake != null)
                    {
                        screenShake.IsShaking();
                    }
                    playerHealthAdded = playerHealth + 5;
                    if (enScript.currentHealth == playerHealth)
                    {
                        Debug.Log("Adding health");
                        playerHealthUpdate = playerHealthAdded + 1;
                        playerHealth = playerHealth + 1;
                    }
                    else
                    {

                    }
                    playerGrowing = true;
                    enScript.isLaunched = true;
                    StartCoroutine(BelatedDeath(col.gameObject));
                    StartCoroutine(Flash(col.gameObject));
                }
            }
            //script for if an enemy outweighs the player
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

    //kills the collided equal enemy after 2 seconds
    IEnumerator BelatedDeath(GameObject enemy)
    {
        if (screenShake != null)
        {
            screenShake.IsShaking();
        }
        Debug.Log("killing this enemy");
        yield return new WaitForSeconds(2);
        Instantiate(deathParticles, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);

        EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
        Debug.Log("Current form is " + enemyScript.currentFormIndex);
        if (enemyScript.currentFormIndex == 8)
        {
            Instantiate(enemyScript.skeletonBonePile, enemy.transform.position, Quaternion.identity);
        }

        Destroy(enemy);
    }

    IEnumerator Flash(GameObject enemy)
    {
        SpriteRenderer spriteRenderer;
        spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        while (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.gray;
            yield return new WaitForSeconds(0.1f);
        }
    }

    //grows the player larger depending on enemy
    public void GrowPlayer(int playerHealthUpdate)
    {
        if ((Mathf.Round(playerHealthUpdate * 100))/100 <  playerHealthUpdate)
        {
            transform.localScale = new Vector2(Mathf.Lerp(transform.localScale.x, playerHealthUpdate, 0.1f), Mathf.Lerp(transform.localScale.y, playerHealthUpdate, 0.1f));
        }
        else
        {
            transform.localScale = new Vector2(playerHealthUpdate,playerHealthUpdate);
            playerGrowing = false;
        }
    }
}
