using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SensorContoller : MonoBehaviour
{
    
    private CheerEstimation cheerEstimation = new CheerEstimation();
    private CountController countText;
    
    public Text testText;
    
    void Start()
    {
        if(Accelerometer.current != null) InputSystem.EnableDevice(Accelerometer.current);
        countText = GameObject.Find("CountText").GetComponent<CountController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Accelerometer.current != null)
        {
            testText.text = ("Attitude: " + Accelerometer.current.acceleration.ReadValue());
            testText.text += ("\n Estimation: " + cheerEstimation.CheerEstimationResult(Accelerometer.current.acceleration.ReadValue()));
            if (cheerEstimation.CheerEstimationResult(Accelerometer.current.acceleration.ReadValue()))
            {
                GameManager.cheersCount++;
                countText.ChangeCountText(GameManager.cheersCount);
            }
        }
        else
        {
            Debug.Log("Attitude: No Attitude Sensor");
        }
        
    }
}
