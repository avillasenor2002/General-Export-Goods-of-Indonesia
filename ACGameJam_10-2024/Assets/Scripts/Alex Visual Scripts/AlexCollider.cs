using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexCollider : MonoBehaviour
{
    public AlexScreenShake alexScreenShake;

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        //Feel free to change the tag to anything
        if(collision.gameObject.tag == "Wall")
        {
            alexScreenShake.IsShaking();
            UnityEngine.Debug.Log("Hit!");
        }
    }
}
