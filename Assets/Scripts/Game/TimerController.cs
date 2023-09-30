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
    public float maxTime = 20.0f;
    
    /// <summary>
    /// タイマーの現在値
    /// </summary>
    private float totalTime;
    
    /// <summary>
    /// 終了後に表示するテキストの最大値
    /// </summary>
    public float MaxfinishTime = 5.0f;

    /// <summary>
    /// 終了のタイマー時間
    /// </summary>
    private float finishTime;
    
    /// <summary>
    /// 終了用のテキストを扱うやつ
    /// </summary>
    public Text FinishText;
    
    /// <summary>
    /// cupのオブジェクト
    /// </summary>
    public GameObject cup;
        
    /// <summary>
    ///  cupのAudioSource
    /// </summary>
    private AudioSource cupAudioSource;
    
    /// <summary>
    /// 終了ようSE
    /// </summary>
    public AudioClip finishAudioClip;
    
    // Start is called before the first frame update
    void Start()
    {
        totalTime = maxTime;
        finishTime = MaxfinishTime;
        FinishText.enabled = false;
        
        cupAudioSource = cup.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (totalTime <= 0.0f)
        {
            // SEを鳴らす
            if (finishTime == MaxfinishTime)
            {
                cupAudioSource.PlayOneShot(finishAudioClip);
            }
            
            // フィニッシュテキストを有効かさせる
            FinishText.enabled = true;
            GameManager.isGameStart = false;
            
            // 終了用のタイマーを減らす
            finishTime -= Time.deltaTime;
        }
        if (totalTime <= 0.0f && finishTime <= 0.0f)
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
