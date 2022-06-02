using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startText : MonoBehaviour
{
    private const float Speed = 1f;
    private const float Length = 0.2f;
    private float runningTime;
    private float yPos = 0f;
    void Update()
    {
        runningTime += Time.deltaTime * Speed;
        yPos = Mathf.Sin(runningTime) * Length;
        this.transform.position = new Vector2(0, yPos+1.5f);
    }
}
