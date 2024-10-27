using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    public BallMovement ball;
    public int pitDamage;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        ball = collision.GetComponent<BallMovement>();
        if (ball != null)
        {
            if (ball.playerHealth < pitDamage)
            {
                Destroy(ball.gameObject);
            }
        }
    }
}
