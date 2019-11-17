using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
//using System.Collections;

public class cat : MonoBehaviour
{
    #region 欄位區域
    [Header("跳躍次數")]
    [Range(1, 10)]
    public int jumpCount = 2;
    [Header("跳躍高度")]
    [Range(10, 1000)]
    public int jump = 100;
    [Header("速度")]
    [Range(0f, 10f)]
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
    private AudioSource Audio; //聲音
    //public AudioSource Audiojump;//跳躍聲音
    //public AudioSource AudioSliding;//滑行聲音
    public AudioClip Audiojump; //AudioClop 跳躍聲音
    public AudioClip AudioSliding; ////AudioClop 滑行聲音
    private SpriteRenderer Sr; //修改顏色
    public Tilemap tileprop;
    public Text Dpdiamond; //顯示鑽石文字
    public int Cpdiamond;  //鑽石整數
    public int CpCherry; //櫻桃整數
    public Text DpCherry; // 顯示櫻桃文字
    public float lose = 1;
    public AudioClip soundDiamond;
    public AudioClip soundCherry;
    #region 欄位區域
    [Header("結算畫面")]
    public GameObject final;
    public Text textdiamond, textCherry, textTime, textTotal;
    //public int ScroedDiamond, ScroreCherry, StTime, Stscore;
    public int[] scores = new int[4];
    #endregion
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "地板")
        {
            isground = true;
        }
        if (collision.collider.name == "櫻桃")
        {
            Eatprop(collision);
            CpCherry++;
            DpCherry.text = CpCherry + "";
        }
  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "障礙物")
        {
            Sprite();

        }
        if (collision.tag == "鑽石")
        {
            Eatdiamond(collision);
            Cpdiamond++;
            Dpdiamond.text = Cpdiamond + "";
        }
        if (collision.name == "死亡區域")
        {
            Debug.Log("死了");    
            hp = 0;
            Dead();
        }


    }

    private void Eatdiamond(Collider2D collision)
    {
        Destroy(collision.gameObject);
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
        Dead();

    }
    private void ShowSprite()
    {
        Sr.enabled = true;
    }
    private void Update()
    {
        MoveCat();
        MoveCom();
        HpLose();
    }
    /// <summary>
    /// 貓的移動方法
    /// </summary>
    private void MoveCat()
    {
        Cat.Translate(speed * Time.deltaTime, 0, 0);

    }
    /// <summary>
    /// 攝影機移動方法
    /// </summary>
    private void MoveCom()
    {
        //欄位.位移(速度*每秒*/1,y,z)
        com.Translate(speed * Time.deltaTime, 0, 0);

    }
    /// <summary>
    /// 跳躍方法
    /// </summary>
    public void Jump()
    {
        if (hp <= 0) return;
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
        if (hp <= 0) return;  //當HP=0 跳出程式
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
    private void Eatprop(Collision2D collision)
    {
        Debug.Log("吃到櫻桃了");
        Vector3 center = Vector3.zero;
        Vector3 hitpoint = collision.contacts[0].point;
        Vector3 normal = collision.contacts[0].normal;

        center.x = hitpoint.x - normal.x * 0.01f;
        center.y = hitpoint.y - normal.y * 0.01f;

        tileprop.SetTile(tileprop.WorldToCell(center), null);

    }
    public void HpLose()
    {
        hp -= Time.deltaTime* lose;
        ig.fillAmount = hp / maxhp;
        Dead();
    }
    private void Dead()
    {
        if (hp <= 0)
        {
            speed = 0;
            Ani.SetBool("死亡", true);
            Final();
        }
    }
    private void Final()
    {
        if (final.activeInHierarchy == false)
        {
            final.SetActive(true);
            StartCoroutine(FinalCaculate(Cpdiamond,0, 100,textdiamond, soundDiamond));
            StartCoroutine(FinalCaculate(CpCherry,1,300, textCherry, soundCherry,Cpdiamond*0.2f));
            int time = (int)Time.timeSinceLevelLoad; // int 轉變整數的型態 小數點去掉
            StartCoroutine(FinalCaculate(time, 2, 200, textTime, AudioSliding, Cpdiamond * 0.2f + CpCherry * 0.2f));
            int total = scores[0] + scores[1] + scores[2] / 100;
            StartCoroutine(FinalCaculate(total, 3, 100, textTotal, AudioSliding, Cpdiamond * 0.2f + CpCherry * 0.2f +time*0.2f));
        }
    }
    
    
   // int IDiamond = 0;
   // int ICherry = 0;
    /// <summary>
    /// 結算鑽石
    /// </summary>
    /// <returns></returns>
    private IEnumerator FinalCaculate(int count, int scoreIndex, int addScore,Text textFinal, AudioClip sound, float wait =0,float waitTime =0.2f)
    {

        yield return new WaitForSeconds(wait);
        //Stdiamond.text = "100";
        //yield return new WaitForSeconds(1);
        //Stdiamond.text = "200";
        //yield return new WaitForSeconds(1);
        //Stdiamond.text = "300";

        while (count > 0)                                                 //當數量>0時執行
        {
            count--;                                                      //鑽石數量-
            scores[scoreIndex] += addScore;                                //分數遞減
            textFinal.text = scores[scoreIndex].ToString();              //更新介面
            Audio.PlayOneShot(sound);
            yield return new WaitForSeconds(waitTime);                    //等待
        }

        if (scoreIndex != 3) scores[3] += scores[scoreIndex];
        if (scoreIndex == 2)
        {
            int total = scores[3] / 100;
            scores[3] = 0;
            StartCoroutine(FinalCaculate(total, 3, 100, textTotal, AudioSliding, 0, 0.05f));
        }
    }
    /// <summary>
    /// 結算櫻桃
    /// </summary>
    /// <returns></returns>
   /* private IEnumerator FinalCherry()
    {

        while (CpCherry > 0)                                                //當數量>0時執行
        {
            CpCherry--;                                                     //櫻桃數量-
            ICherry += 100;                                                 //分數遞減
            textCherry.text = ICherry.ToString();                           //更新介面
            yield return new WaitForSeconds(1);                             //等待
        }
    }
    */
}





