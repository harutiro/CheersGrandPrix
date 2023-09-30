using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaleteWaterGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //tag waterがついたオブジェクトと衝突したら消す
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("衝突した");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "water")
        {
            Destroy(collision.gameObject);
        }
    }
}
