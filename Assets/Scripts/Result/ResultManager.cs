using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultGameManager : MonoBehaviour
{

    public Text CountText;
    public Text PointText;
    public Text LiquidText;
    public Text StrongText;
    public Text MessageText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetTextScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextScore()
    {
        CountText.text = GameScoreStatic.Count.ToString() + " 回乾杯した。";
        PointText.text = GameScoreStatic.Score.ToString() + " 点";
        LiquidText.text = GameScoreStatic.WaterPercent.ToString() + "パーセント残った";
        StrongText.text = GameScoreStatic.Weak + " 弱 "　+ GameScoreStatic.Normal + " 普 " + GameScoreStatic.Strong + " 強";
        MessageText.text = messageSwitch(GameScoreStatic.Score);
    }

    private string messageSwitch(int point)
    {
        // ポイントに応じて対応するメッセージを取得
        int[] thresholds = { 0, 30, 60, 90 }; // 閾値を定義
        for (int i = thresholds.Length - 1; i >= 0; i--)
        {
            if (point > thresholds[i])
            {
                return ResultMessageModel.messageDictionary[i];
            }
        }

        return ResultMessageModel.messageDictionary[0]; // デフォルトのメッセージ
    }
    
    public void OnTouchBackButton()
    {
        // シーンの読み込み
        SceneController.LoadScene(SceneController.SceneName.Main);
        GameScoreStatic.Rest();
    }
}
