using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTouchBackButton()
    {
        // シーンの読み込み
        SceneController.LoadScene(SceneController.SceneName.Main);
    }
}
