using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static float SineInOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        return -max / 2 * (float)(Mathf.Cos(t * Mathf.PI / totaltime) - 1) + min;
    }

    public static float BackInOut(float t, float totaltime, float min, float max, float s)
    {
        max -= min;
        s *= 1.525f;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * (t * t * ((s + 1) * t - s)) + min;

        t = t - 2;
        return max / 2 * (t * t * ((s + 1) * t + s) + 2) + min;
    }

    public static float QuartIn(float t ,float totaltime, float min ,float max)
    {
        max -= min;
        t /= totaltime;
        return max * t * t * t * t + min;
    }

}
