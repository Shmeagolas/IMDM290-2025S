using System;
using Unity.VisualScripting;
using UnityEngine;

public class Heart4 : MonoBehaviour
{
    private int numPoints;
    private float heartMaxSize, heartMinSize, breathePeriod;
    private GameObject[] points;
    private Color[] colors;
    private Color beatStartColor, beatEndColor;
    float curTime, beatTime, beatPeriod, beatStartSize, beatGoalSize, beatEndSize;
    private bool isBeating;
    public void MakeHeart(int nP, float hMax, float hMin, float bTime, Color brCol1, Color brCol2, Color btCol)
    {
        numPoints = nP;
        heartMaxSize = hMax;
        heartMinSize = hMin;
        breathePeriod = bTime / (2 * Mathf.PI);
        points = new GameObject[numPoints];
        colors = new Color[3];
        colors[0] = brCol1;
        colors[1] = brCol2;
        colors[2] = btCol;

        curTime = 0f;

        Material mat = new Material(Shader.Find("Unlit/Color"));

        for (int i = 0; i < numPoints; i++)
        {
            float t = i * 2 * Mathf.PI / numPoints;
            points[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            points[i].transform.position = new Vector3(CalculateX(hMin, t), CalculateY(hMin, t), 0);
            points[i].GetComponent<Renderer>().material = mat;
        }
    }



    void Update()
    {

        curTime += Time.deltaTime;
        

        if(isBeating)
        {
            beatTime += Time.deltaTime;
            UpdateHeart(BeatSize(beatTime), BeatColor(beatTime));
        }
        else
        {
            UpdateHeart(BreatheSize(curTime), BreatheColor(curTime));
        }

    }

    public void Beat(float beatLength, float beatSize)
    {
        isBeating = true;
        beatTime = 0f;
        beatPeriod = beatLength / Mathf.PI;
        beatStartSize = BreatheSize(curTime);
        beatGoalSize = beatSize * BreatheSize(curTime);
        beatEndSize = BreatheSize(curTime + beatLength);
        beatStartColor = BreatheColor(curTime);
        beatEndColor = BreatheColor(curTime + beatLength);
    }

    private float BeatLerpFraction(float beatTime)
    {
        float beatTheta = beatTime / beatPeriod;
        return Mathf.Sin(beatTheta);
    }

    private float BeatSize(float beatTime)
    {
        float length = beatPeriod * Mathf.PI;
       
        if(beatTime <= length / 2f)
        {
            return RoundToHundreths(Mathf.Lerp(beatStartSize, beatGoalSize, BeatLerpFraction(beatTime)));
        }
        else
        {
            if(beatTime > length)
            {
                isBeating = false;
            }
            return RoundToHundreths(Mathf.Lerp(beatEndSize, beatGoalSize, BeatLerpFraction(beatTime)));
        }
    }

    private Color BeatColor(float beatTime)
    {
        float length = beatPeriod * Mathf.PI;
       
        if(beatTime <= length / 2f)
        {
            return Color.Lerp(beatStartColor, colors[2], BeatLerpFraction(beatTime));
        }
        else
        {
            return Color.Lerp(beatEndColor, colors[2], BeatLerpFraction(beatTime));
        }
    }
    private float BreatheLerpFraction(float time)
    {
        float breatheTheta = time / breathePeriod;
        return 0.5f * Mathf.Sin(breatheTheta) + 0.5f;
    }
    private float BreatheSize(float time)
    {
        float breatheSize = Mathf.Lerp(heartMinSize, heartMaxSize, BreatheLerpFraction(time));
        return RoundToHundreths(breatheSize);
    }
    private Color BreatheColor(float time)
    {
        return Color.Lerp(colors[0], colors[1], BreatheLerpFraction(time));
    }

    private void UpdateHeart(float size, Color color)
    {
        for (int i = 0; i < numPoints; i++)
        {
            float t = i * 2 * Mathf.PI / numPoints;
            points[i].transform.position = new Vector3(CalculateX(size, t), CalculateY(size, t), 0);
            points[i].GetComponent<Renderer>().material.color = color;
        }
    }

    private void UpdatePointColor(GameObject point)
    {

    }

     private float CalculateX(float radius, float theta)
    {
        //x = root(2) * sin^3(theta)
        return radius * (Mathf.Sqrt(2f) * Mathf.Pow(Mathf.Sin(theta), 3f));
    }

    private float CalculateY(float radius, float theta)
    {
        //y = - cos^3(theta) - cos^2(theta) + 2cos(theta)
        float cos = Mathf.Cos(theta);
        return radius * (-1f * Mathf.Pow(cos, 3f) - Mathf.Pow(cos, 2f) + (2f * cos));
    }
    private float RoundToHundreths(float x)
    {
        return Mathf.Round(x * 100) / 100f;
    }
}
