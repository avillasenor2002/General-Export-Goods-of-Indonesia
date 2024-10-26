using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexScreenShake : MonoBehaviour
{
    public float elapseTime = 0;
    public float durationTime = 1f;
    //The animationCurve control the strength of shaking, editable in the unity scene
    public AnimationCurve shakeCurve;
    //Testing the shaking by click the start check box
    public bool isShaking = false;

    public void IsShaking()
    {
        if(!isShaking)
        {
            elapseTime = 0;
            StartCoroutine(Shaking());
        }
    }
    public IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        
        while (elapseTime < durationTime)
        {
            elapseTime += Time.deltaTime;
            float strength = shakeCurve.Evaluate(elapseTime / durationTime);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
        isShaking = false;
    }
}
