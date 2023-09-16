using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    /// <summary>
    /// タイマーのテキスト
    /// </summary>
    public Text timerText;
    
    /// <summary>
    /// タイマーの最大値
    /// </summary>
    public float maxTime = 60.0f;
    
    /// <summary>
    /// タイマーの現在値
    /// </summary>
    private float totalTime;
        
    
    // Start is called before the first frame update
    void Start()
    {
        totalTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalTime <= 0.0f)
        {
            // タイマーが0になったらゲーム終了
            SceneController.LoadScene(SceneController.SceneName.Result);
        }
        
        if(GameManager.isGameStart)
        {
            totalTime -= Time.deltaTime;
            timerText.text = "残り " + totalTime.ToString("F0") + " 秒";
        }
        
    }
}
