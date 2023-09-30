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
    private CheerRotateEstimation cheerRotateEstimation = new CheerRotateEstimation();
    // private OtherFileStorage otherFileStorage = new OtherFileStorage();
    public CountController countText;
    public GameObject cup;
    private AudioSource cupAudioSource;
    
    public AudioClip bombAudioClip;
    public AudioClip cheerAudioClip;
    public AudioClip aerAudioClip;
    
    // 重力ベクトルの初期化
    private Vector3 gravity = Vector3.down * 9.8f;
    
    // ジャイロスコープの回転データ
    private Vector3 gyroRotation = Vector3.zero;
    
    // csv用の相対時間を計測するための変数
    private float timer = 0.0f;
    
    // message用のテキスト
    public Text MessageText;
    
    // Debug用のテキスト
    public Text testText;

    private GameObject bomb;

    private GameObject[] defaultWaters;
    private GameObject[] waters;
    
    void Start()
    {
        if(Accelerometer.current != null) InputSystem.EnableDevice(Accelerometer.current);
        if(Gyroscope.current != null) InputSystem.EnableDevice(Gyroscope.current);
        if(AttitudeSensor.current != null) InputSystem.EnableDevice(AttitudeSensor.current);
        if(LinearAccelerationSensor.current != null) InputSystem.EnableDevice(LinearAccelerationSensor.current);
        
        cupAudioSource = cup.GetComponent<AudioSource>();
        
        // tagからbombを取得
        bomb = GameObject.FindGameObjectWithTag("Bomb");
        
        // 一番最初の水の数を取得
        defaultWaters = GameObject.FindGameObjectsWithTag("water");
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Accelerometer.current != null) InputSystem.EnableDevice(Accelerometer.current);
        if(Gyroscope.current != null) InputSystem.EnableDevice(Gyroscope.current);
        if(AttitudeSensor.current != null) InputSystem.EnableDevice(AttitudeSensor.current);
        if(LinearAccelerationSensor.current != null) InputSystem.EnableDevice(LinearAccelerationSensor.current);
        
        if (
            Accelerometer.current != null &&
            Gyroscope.current != null &&
            AttitudeSensor.current != null &&
            LinearAccelerationSensor.current != null &&
            GameManager.isGameStart
        ) {
            
            // 水の数を取得
            waters = GameObject.FindGameObjectsWithTag("water");
            
            // bombを初期化する
            bomb.GetComponent<CircleCollider2D>().radius = 0.0f;

            // 加速度のcsvデータ
            timer += Time.deltaTime;
            string debugText = "";
            
            // 生データを取得
            Vector3 gyro = Gyroscope.current.angularVelocity.ReadValue();
            Vector3 Acceleration = LinearAccelerationSensor.current.acceleration.ReadValue();
            
            // 傾きによってジョッキを傾ける
            float cheerRotateResult = cheerRotateEstimation.CheerEstimationRotateResult(gyro.z * 10f);
            cup.transform.rotation = Quaternion.Euler(0, 0, cheerRotateResult);
            
            // csvデータの作成
            // debugText += timer + "," + Acceleration.x+ "," + Acceleration.y + "," + Acceleration.z;
            
            // デバック用の出力
            testText.text = ("Attitude: " + debugText);
            Debug.Log("Attitude: " + debugText);
            // otherFileStorage.doLog(debugText);
            
            Debug.Log("液体数" + waters.Length);
            
            // 状態の推定
            CheerEstimationModel result = cheerEstimation.CheerEstimationResult(Acceleration);
            if (result != CheerEstimationModel.None && result != CheerEstimationModel.Missing && result != CheerEstimationModel.Weak)
            {
                int waterPercent = (int) ((float)waters.Length / (float)defaultWaters.Length * 100);
                
                GameScoreStatic.Set(result,waterPercent);
                countText.ChangeCountText(GameScoreStatic.Score);
                MessageText.text = resultProcessing(result);
            }
            else if(result == CheerEstimationModel.Missing)
            {
                bomb.GetComponent<CircleCollider2D>().radius = 2f;
                MessageText.text = resultProcessing(result);
            }
            else if (result == CheerEstimationModel.Weak)
            {
                MessageText.text = resultProcessing(result);
            }
        }
        else
        {
            Debug.Log("Attitude: No Attitude Sensor");
        }
    }

    string resultProcessing(CheerEstimationModel result)
    {
        if (result == CheerEstimationModel.None)
        {
            return "";
        }
        else if (result == CheerEstimationModel.Weak)
        {
            cupAudioSource.PlayOneShot(aerAudioClip);
            return "勇気を出して、もっと力を";            
        }
        else if (result == CheerEstimationModel.Normal)
        {
            cupAudioSource.PlayOneShot(cheerAudioClip);
            return "乾杯";
        }
        else if (result == CheerEstimationModel.Strong)
        {
            cupAudioSource.PlayOneShot(cheerAudioClip);
            return "たのしーく乾杯";
        }
        else if (result == CheerEstimationModel.Missing)
        {
            cupAudioSource.PlayOneShot(bombAudioClip);
            return "強すぎ！！";
        }
        else
        {
            return "";
        }
    }
    
}


//　ここでスマホの角度をそのまま取得してジョッキを回すが、あまり精度がよくない。
// Quaternion attitude = AttitudeSensor.current.attitude.ReadValue();
//
// // z軸以外の回転をなくす
// // 90度回転させる
// if(attitude.x != 0.0f && attitude.y != 0.0f && attitude.z != 0.0f)
// {
//     cup.transform.rotation = Quaternion.Euler(0, 0, attitude.eulerAngles.z + 90f);
// }