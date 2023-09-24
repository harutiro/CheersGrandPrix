using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameScoreStatic
{
    public static int Count = 0;
    public static int Score = 0;
    public static int Strong = 0;
    public static int Normal = 0;
    public static int Weak = 0;
    public static double WaterPercent = 0;

    public static void Set(CheerEstimationModel result, double waterPercent)
    {
        Count++;
        WaterPercent = waterPercent;
        if (result == CheerEstimationModel.Weak)
        {
            Weak++;
            Score += (int) Math.Round(waterPercent / 100 * 1);
        }
        if (result == CheerEstimationModel.Normal)
        {
            Normal++;
            Score += (int) Math.Round(waterPercent / 100 * 2);
        }
        if (result == CheerEstimationModel.Strong)
        {
            Strong++;
            Score += (int) Math.Round(waterPercent / 100 * 3);
        }
    }

    public static void Rest()
    {
        Score = 0;
        Strong = 0;
        Normal = 0;
        Weak = 0;
        Count = 0;
        WaterPercent = 0;
    }
}
