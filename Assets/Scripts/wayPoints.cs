
using System;
using UnityEngine;

public class wayPoints : MonoBehaviour
{
    public static Transform[] points;

     void Awake()
    {
        // find all children under this game object [ which this script is attached to ]
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i]= transform.GetChild(i);
        }
    }
}
