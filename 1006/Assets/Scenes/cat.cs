using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cat : MonoBehaviour
{
    #region 欄位區域
    [Header("跳躍次數")] [Range(1, 10)]
    public int jumpCount = 2;
    [Header("跳躍高度")] [Range(10, 1000)]
    public int jump = 100;
    [Header("速度")] [Range(0f, 10f)]
    public float speed = 2.5f;
    [Header("是否在地面")]
    public bool isground = false;
    [Header("名稱")]
    public string cat2 = "yaya";
    [Header("血量")]
    public float hp = 500; //血量
    private float maxhp; //最大血量
    public float Injured; //受傷

    public Image ig;  //圖片
    private Transform Cat, com;
    #endregion
    private Animator Ani; //動畫
    private CapsuleCollider2D cc2D; //膠囊
    private Rigidbody2D rb2; //鋼體
    AudioSource Audio; //聲音
    //public AudioSource Audiojump;//跳躍聲音
    //public AudioSource AudioSliding;//滑行聲音
    public AudioClip Audiojump; //AudioClop 跳躍聲音
    public AudioClip AudioSliding; ////AudioClop 滑行聲音
    private SpriteRenderer Sr; //修改顏色
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "地板")
        {
            isground = true;
        }
 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "障礙物")
        {
            Sprite();

        }
    }


    private void Start()
    {
        Ani = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
        cc2D = GetComponent<CapsuleCollider2D>();
        com = GameObject.Find("Main Camera").transform;
        Cat = GetComponent<Transform>();
        Audio = GetComponent<AudioSource>();
        Sr = GetComponent<SpriteRenderer>();
        maxhp = hp;
        // Find 尋找取得值
        // transform 取得位移旋轉縮放


    }
    private void Sprite()
    {
        Debug.Log("受傷");
        Sr.enabled = false;
        Invoke("ShowSprite", 0.2f);
        hp = hp - Injured;
        ig.fillAmount = hp / maxhp;

    }
    private void ShowSprite()
    {
        Sr.enabled = true;
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
        if (isground == true)
        {
            Ani.SetBool("跳躍", true);
            rb2.AddForce(new Vector2(0, jump));
            isground = false;
            //Audiojump.Play();
            Audio.PlayOneShot(Audiojump);
        }
    }
    public void isGround()
    {
        Ani.SetBool("滑行", true);
        cc2D.offset = new Vector2(-0.008f, -0.5f);
        cc2D.size = new Vector2(0.5f, 0.5f);
        //AudioSliding.Play();
        Audio.PlayOneShot(AudioSliding);
    }
    /// <summary>
    /// 重新設定布林值 重設膠囊範圍
    /// </summary>
    public void ResetAnimator()
    {
        Ani.SetBool("跳躍", false);
        Ani.SetBool("滑行", false);
        cc2D.offset = new Vector2(-0.008f, -0.1f);
        cc2D.size = new Vector2(0.5f, 1.4f);
    }
}




