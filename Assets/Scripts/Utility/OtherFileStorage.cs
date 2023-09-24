using System.IO;
using UnityEngine;

public class OtherFileStorage
{
    private bool fileAppend = true; //true=追記, false=上書き
    public string fileName = "acc.csv";
    public string date = System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
    private string filePath = Application.dataPath + "/SensorData";
    
    public void doLog(string text)
    {
        string path = filePath + "/" + date + "-"+ fileName;
        writeText(text, path);
    }

    private void writeText(string text, string path)
    {
        StreamWriter sw = new StreamWriter(path,fileAppend);// TextData.txtというファイルを新規で用意
        sw.WriteLine(text);// ファイルに書き出したあと改行
        sw.Flush();// StreamWriterのバッファに書き出し残しがないか確認
        sw.Close();// ファイルを閉じる
    }
}