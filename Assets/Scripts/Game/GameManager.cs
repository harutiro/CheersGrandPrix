using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    /// <summary>
    /// ゲームが開始したかどうか
    /// </summary>
    public static bool isGameStart = true;
    
    /// <summary>
    /// 乾杯のカウント
    /// </summary>
    public static int cheersCount = 0;
    
    /// <summary>
    /// 強い乾杯のカウント
    /// </summary>
    public static int strongCheersCount = 0;
    
    /// <summary>
    /// 普通の乾杯のカウント
    /// </summary>
    public static int normalCheersCount = 0;
    
    /// <summary>
    /// 弱い乾杯のカウント
    /// </summary>
    public static int weakCheersCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
