using System;
using Unity.VisualScripting;
using UnityEngine;

public class Circle : MonoBehaviour
{
    static int numPoints = 100,
        radius = 5;
    static float scale = .25f;

    GameObject[] Points;

    void Start()
    {
        Points = new GameObject[numPoints];
        for(int i = 0; i < numPoints; i++)
        {   
            float theta = i * 2 * Mathf.PI / numPoints;
            Points[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Points[i].transform.position = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0);
            Points[i].transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
