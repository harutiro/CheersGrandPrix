using System;
using System.Collections.Generic;
using UnityEngine;

public class CheerEstimation
{
    private List<float> normList = new List<float>();
    private bool isCheered = false;
    private CheerEstimationModel maxCheerEstimation = CheerEstimationModel.None;

    /// <summary>
    /// 姿勢からノルムを計算します。
    /// </summary>
    private float CalculateNorm(Vector3 attitude)
    {
        return Mathf.Sqrt(attitude.x * attitude.x + attitude.y * attitude.y + attitude.z * attitude.z);
    }

    /// <summary>
    /// ノルムリストを更新し、最大10個のノルムを保持します。
    /// </summary>
    private void UpdateNormList(float norm)
    {
        normList.Add(norm);
        if (normList.Count > 10)
        {
            normList.RemoveAt(0);
        }
    }

    /// <summary>
    /// リスト内のノルムの平均値を計算します。
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
    /// 平均値に基づいて応援度合いを評価し、結果を返します。
    /// </summary>
    private CheerEstimationModel DetermineCheerEstimation(float average)
    {

        Debug.Log(average);
        if (average > 2.0f)
        {
            Debug.Log("Missing");
            maxCheerEstimation = CheerEstimationModel.Missing;
        }
        else if (average > 1.5f)
        {
            Debug.Log("Strong");
            if (maxCheerEstimation != CheerEstimationModel.Missing)
            {
                maxCheerEstimation = CheerEstimationModel.Strong;
            }
        }
        else if (average > 1.0f)
        {
            Debug.Log("Normal");
            if (maxCheerEstimation != CheerEstimationModel.Missing && maxCheerEstimation != CheerEstimationModel.Strong)
            {
                maxCheerEstimation = CheerEstimationModel.Normal;
            }
        }
        else if (average > 0.5f)
        {
            Debug.Log("Weak");
            if (
                maxCheerEstimation != CheerEstimationModel.Missing &&
                maxCheerEstimation != CheerEstimationModel.Strong &&
                maxCheerEstimation != CheerEstimationModel.Normal
            ) {
                maxCheerEstimation = CheerEstimationModel.Weak;
            }
        }
        else
        {
            CheerEstimationModel temp = maxCheerEstimation;
            maxCheerEstimation = CheerEstimationModel.None;
            return temp;
        }

        return CheerEstimationModel.None;
    }

    /// <summary>
    /// 姿勢から応援度合いを評価し、結果を返します。
    /// </summary>
    public CheerEstimationModel CheerEstimationResult(Vector3 attitude)
    {
        float norm = CalculateNorm(attitude);
        UpdateNormList(norm);

        if (normList.Count >= 10)
        {
            float average = CalculateAverage(normList);
            return DetermineCheerEstimation(average);
        }

        return CheerEstimationModel.None;
    }
}
