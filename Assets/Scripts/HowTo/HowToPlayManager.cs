using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnClickBackButton()
    {
        // MainSceneに移動
        SceneController.LoadScene(SceneController.SceneName.Main);
    }
}
