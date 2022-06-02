using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class LineImage : MonoBehaviour
{
    private RectTransform imageRectTransform;
    public float lineWidth = 1;
    public Transform pointA;
    public Transform pointB;

    LineRenderer line;
    // Use this for initialization
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void Init(Transform pointA, Transform pointB)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        Color lineColor = Color.yellow;
        lineColor.a = 1f;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.materials[0].color = lineColor;
        line.startColor = lineColor;
        line.endColor = lineColor;
        Destroy(gameObject, 0.2f);
    }
    private void Update()
    {
        if (pointA == null || pointB == null)
        {
            Destroy(gameObject);
            return;
        }
        line.SetPositions(new Vector3[] { pointA.position + Vector3.forward, pointB.position + Vector3.forward });
    }

}