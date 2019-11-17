using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //引用場景管理 API
using System.Collections;


public class GameManager : MonoBehaviour
{
    public Text textLoading;
    public Image imageLoading;
    /// <summary>
    /// 重新開始
    /// </summary>
   public void Replay(string scene)
    {
        SceneManager.LoadScene(scene);   //場景管理.載入場景("場景名稱")
    }
    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void Quit()
    {
        Application.Quit(); //離開畫面
    }
   ///<summary>
   ///開始載入場景
   ///</summary>
   public void StarLoading()
    {
        StartCoroutine(Loading());
    }
   public IEnumerator Loading()
    {
       AsyncOperation ao = SceneManager.LoadSceneAsync("關卡1"); //取得載入場景的資料
       ao.allowSceneActivation = false;                          //取消載入

        while(ao.isDone == false)                                //當前場景載入尚未完成
        {
            textLoading.text = ao.progress /0.9f *100 + " / 100"; //更新文字介面
            imageLoading.fillAmount = ao.progress / 0.9f;         //更新吧條
            yield return null;                                    //等待
            //yield return new WaitForSeconds(0.1f);        
            if(ao.progress == 0.9f && Input.anyKey)               //如果 進度 == 0.9並且玩家按下任意鍵

            {
                ao.allowSceneActivation = true;                   //開始載入
                
            }
        }         
   }
}
