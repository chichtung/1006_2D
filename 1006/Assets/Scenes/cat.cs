using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour
{
    #region 欄位區域
    [Header("跳躍次數")] [Range(1, 10)]
    public int jumpCount = 2;
    [Header("跳躍高度")] [Range(10, 100)]
    public int jump = 100;
    [Header("速度")] [Range(2.0f, 3.0f)]
    public float speed = 2.5f;
    [Header("是否在地面")]
    public bool isground = true;
    [Header("名稱")]
    public string cat2 = "yaya";
    public Transform Cat, com;
    #endregion

    private void Start()
    {
        
    }
    private void Update()
    {
        MoveCat();
        MoveCom();
    }
    /// <summary>
    /// 貓的移動方法
    /// </summary>
    private void MoveCat()
    {
        Cat.Translate(speed*Time.deltaTime, 0, 0);

    }
    /// <summary>
    /// 攝影機移動方法
    /// </summary>
    private void MoveCom()
    {
        //欄位.位移(速度*每秒*/1,y,z)
        com.Translate(speed*Time.deltaTime, 0, 0);

    }
    /// <summary>
    /// 跳躍方法
    /// </summary>
    public void Jump()
    {

    }
    public void isGround()
    {

    }
}




