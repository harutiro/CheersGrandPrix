using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnClickRankingButton()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(0);
    }
    
    public void OnClickStartButton()
    {
        // GameSceneに移動
        SceneController.LoadScene(SceneController.SceneName.Game);
        GameManager.isGameStart = true;
    }
}
