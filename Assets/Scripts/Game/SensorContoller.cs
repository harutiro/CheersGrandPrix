using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class SensorContoller : MonoBehaviour
{
    
    private CheerEstimation cheerEstimation = new CheerEstimation();
    private OtherFileStorage otherFileStorage = new OtherFileStorage();
    public CountController countText;
    public GameObject cup;
    
    // 重力ベクトルの初期化
    private Vector3 gravity = Vector3.down * 9.8f;
    
    // ジャイロスコープの回転データ
    private Vector3 gyroRotation = Vector3.zero;
    
    // csv用の相対時間を計測するための変数
    private float timer = 0.0f;
    
    // 出力用のテキスト
    public Text testText;
    
    void Start()
    {
        if(Accelerometer.current != null) InputSystem.EnableDevice(Accelerometer.current);
        if(Gyroscope.current != null) InputSystem.EnableDevice(Gyroscope.current);
        if(AttitudeSensor.current != null) InputSystem.EnableDevice(AttitudeSensor.current);
        if(LinearAccelerationSensor.current != null) InputSystem.EnableDevice(LinearAccelerationSensor.current);
    }
    // Update is called once per frame
    void Update()
    {
        if(Accelerometer.current != null) InputSystem.EnableDevice(Accelerometer.current);
        if(Gyroscope.current != null) InputSystem.EnableDevice(Gyroscope.current);
        if(AttitudeSensor.current != null) InputSystem.EnableDevice(AttitudeSensor.current);
        if(LinearAccelerationSensor.current != null) InputSystem.EnableDevice(LinearAccelerationSensor.current);
        
        if (Accelerometer.current != null && Gyroscope.current != null && AttitudeSensor.current != null && LinearAccelerationSensor.current != null)
        {
            timer += Time.deltaTime;

            // 加速度のcsvデータ
            string debugText = "";
            
            // 生の加速度データを取得
            // Vector3 acc = Accelerometer.current.acceleration.ReadValue();
            Vector3 gyro = Gyroscope.current.angularVelocity.ReadValue();
            // 重力加速度をなくす
            Vector3 linnearAcceleration = LinearAccelerationSensor.current.acceleration.ReadValue();
            
            Quaternion attitude = AttitudeSensor.current.attitude.ReadValue();
            
            // z軸以外の回転をなくす
            // 90度回転させる
            if(attitude.x != 0.0f && attitude.y != 0.0f && attitude.z != 0.0f)
            {
                cup.transform.rotation = Quaternion.Euler(0, 0, attitude.eulerAngles.z + 90f);
            }
            
            // csvデータの作成
            debugText += timer + "," + attitude.x+ "," + attitude.y + "," + attitude.z;
            
            // デバック用の出力
            // testText.text = ("Attitude: " + debugText);
            Debug.Log("Attitude: " + debugText);
            otherFileStorage.doLog(debugText);
            
            // 状態の推定
            CheerEstimationModel result = cheerEstimation.CheerEstimationResult(linnearAcceleration);
            if (result != CheerEstimationModel.None && result != CheerEstimationModel.Missing)
            {
                GameManager.cheersCount++;
                countText.ChangeCountText(GameManager.cheersCount);
                testText.text += ("\n Estimation: " + result);
            }
            else if(result == CheerEstimationModel.Missing)
            {
                testText.text = "失敗";
                testText.text += ("\n Estimation: " + result);
            }
        }
        else
        {
            Debug.Log("Attitude: No Attitude Sensor");
        }
    }
}
