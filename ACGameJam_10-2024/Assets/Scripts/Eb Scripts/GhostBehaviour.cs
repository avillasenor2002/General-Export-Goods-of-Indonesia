using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// script created by eb!
// this script controls the special behaviour of the 'ghost' type enemies
public class GhostBehaviour : MonoBehaviour
{
    public bool corporealOnEvenTurns = true;
    public SpriteRenderer sprite;
    public CircleCollider2D circleCollider;

    private TurnManager turnManager;

    void Awake()
    {
        turnManager = GameObject.Find("GameManager").GetComponent<TurnManager>();
    }

    void Start()
    {
        turnManager.OnInputCountChanged += InputCountChanged;
    }

    // is called when the input count changes
    // effects the ghost depending on if count is even or odd
    public void InputCountChanged(int inputCount)
    {
        // input count is odd
        if(inputCount % 2 != 0)
        {
            if (corporealOnEvenTurns)
            {
                MakeIncorporeal();
            }
            else
            {
                MakeCorporeal();
            }
        }
        else // input count is even
        {
            if (corporealOnEvenTurns)
            {
                MakeCorporeal();
            }
            else
            {
                MakeIncorporeal();
            }
        }
    }

    // makes the player able to hit the ghost
    private void MakeCorporeal()
    {
        circleCollider.enabled = true;
        sprite.color = new Color(1f,1f,1f,1f);
    }

    // makes the player unable to hit the ghost
    private void MakeIncorporeal()
    {
        circleCollider.enabled = false;
        sprite.color = new Color(1f, 1f, 1f, 0.5f);
    }
}
