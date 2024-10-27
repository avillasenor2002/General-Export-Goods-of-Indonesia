using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
    private int countDown = 3;
    private int startTurn = -1;

    private TurnManager turnManager;

    public GameObject skeletonPrefab;

    void Awake()
    {
        turnManager = GameObject.Find("GameManager").GetComponent<TurnManager>();
    }

    void Start()
    {
        turnManager.OnInputCountChanged += InputCountChanged;
    }

    // is called when the input count changes
    // if skeleton is dead, count down
    // if countdown is reached, revive skeleton
    public void InputCountChanged(int inputCount)
    {
        if (startTurn == -1)
        {
            startTurn = inputCount;
        }

        if(inputCount - startTurn >= countDown)
        {
            Instantiate(skeletonPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
