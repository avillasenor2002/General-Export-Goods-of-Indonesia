using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexKillPlayer : MonoBehaviour
{
    public string Tag;

    private void OnCollisionEnter2D(Collision2D collision) 
    {

            Debug.Log("Hit!");
            Destroy(collision.gameObject);

    }
}
