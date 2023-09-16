using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // シーン名の定義
    public enum SceneName
    {
        Main,
        Game,
        Result
    }

    public static void LoadScene(SceneName sceneName)
    {
        // シーンの読み込み
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName.ToString());
    }
}
