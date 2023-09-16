using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerEstimation
{
    public bool CheerEstimationResult(Vector3 acceleration)
    {
        double norm = Math.Sqrt(acceleration.x * acceleration.x + acceleration.y * acceleration.y + acceleration.z * acceleration.z);
        if (norm > 5.0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
