using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    public int currentSize = 1;

    private TurnManager turnManager;
    private EnemyScript enemyScript;

    void Awake()
    {
        turnManager = GameObject.Find("GameManager").GetComponent<TurnManager>();
        enemyScript = this.GetComponent<EnemyScript>();
    }

    void Start()
    {
        turnManager.OnInputCountChanged += InputCountChanged;
    }

    // is called when the input count changes
    // grows in size every 2 turns
    public void InputCountChanged(int inputCount)
    {
        if (inputCount == 0) { return; }

        // every 3rd input count
        if (inputCount % 3 == 0)
        {
            GrowSlime();
        }
    }

    // grows the slime's size by 1
    private void GrowSlime()
    {
        if(currentSize >= 5)
        {
            return;
        }

        Debug.Log("Slime grows!");
        currentSize++;
        enemyScript.currentHealth++;
        this.transform.localScale = new Vector3(currentSize, currentSize, 1);
    }
}
