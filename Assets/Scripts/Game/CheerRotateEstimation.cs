using System.Collections.Generic;
using UnityEngine;

public class CheerRotateEstimation
{
    /// <summary>
    /// ジョッキの回転角度のリスト
    /// </summary>
    private List<float> rotateList = new List<float>();
    
    /// <summary>
    /// ロテートリストを更新し、最大10個のノルムを保持します。
    /// </summary>
    private void UpdateRotateList(float norm)
    {
        rotateList.Add(norm);
        if (rotateList.Count > 10)
        {
            rotateList.RemoveAt(0);
        }
    }

    /// <summary>
    /// ロテートリスト内の平均値を計算します。
    /// </summary>
    private float CalculateAverage(List<float> list)
    {
        float sum = 0;
        foreach (float n in list)
        {
            sum += n;
        }
        return sum / list.Count;
    }
    
    /// <summary>
    /// ロテートの平均の大きさを出力する。
    /// </summary>
    public float CheerEstimationRotateResult(float gyroZ)
    {
        UpdateRotateList(gyroZ);

        if (rotateList.Count >= 10)
        {
            float average = CalculateAverage(rotateList);
            return average;
        }

        return 0.0f;

    }

}