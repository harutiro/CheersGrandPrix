using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountController : MonoBehaviour
{
    
    public Text countText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ChangeCountText(int count)
    {
        countText.text = "乾杯ポイント　"　+ count;
    }
}
