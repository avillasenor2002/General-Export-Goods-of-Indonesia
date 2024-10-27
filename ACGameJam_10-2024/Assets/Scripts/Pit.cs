using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    public BallMovement ball;
    public EnemyScript enemy;
    public int pitDamage;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        ball = collision.GetComponent<BallMovement>();
        enemy = collision.GetComponent<EnemyScript>();
        if (ball != null)
        {
            if (ball.playerHealth < pitDamage)
            {
                Destroy(ball.gameObject);
            }
        }
        if (enemy != null)
        {
            Destroy(enemy.gameObject);
        }
    }
}
