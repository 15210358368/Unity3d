using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Login : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetString("PNG", null);
    }
    // 输入账号密码
    public InputField account;
    public InputField password;
    // 注册时账号密码
    public InputField register_account;
    public InputField register_password;

    public GameObject LoginWin;
    public GameObject registerWin;
    public GameObject ImageWin;

    public Transform ImageParn;

    /// <summary>
    /// 注册按钮发生事件
    /// </summary>
    public void register()
    {
        string account = register_account.text;

        string password = register_password.text;
        // 如果不为空写入注册表中
        if (account != null && password != null)
        {
            PlayerPrefs.SetString(account, password);
            registerWindow(true);
        }
    }
    public void Logins()
    {

    }
    //退出
    public void Quit()
    {
        Application.Quit();
    }
    //跳转登录窗口
    public void registerWindow(bool isopen)
    {
        LoginWin.SetActive(isopen);
        registerWin.SetActive(!isopen);
    }
    //登录窗口 根据输入的账号密码从注册表中获得 如果密码相同就跳转游戏
    public void LoginWindow(bool isopen)
    {
        //SceneManager.LoadScene(1);

        string accountstr = account.text;
        string passwordstr = password.text;
        if (accountstr != null && passwordstr != null)
        {
            string number = PlayerPrefs.GetString(accountstr);

            if (number != null && int.Parse(passwordstr) == int.Parse(number))
            {
                SceneManager.LoadScene("xg");
            }
        }
    }
    public void ShowImages()
    {
        ImageWin.SetActive(true);
        LoginWin.SetActive(false);
        registerWin.SetActive(false);
        LoadImage();
    }
    public void QuitImages()
    {
        ImageWin.SetActive(false);
        LoginWin.SetActive(true);
        registerWin.SetActive(false);
    }
    public void LoadImage()
    {
        //    string path = null;
        //    string filename = "PNG";
        //    // 选择路径
        //    //Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer ||
        //    if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        //    {
        //        path = Application.dataPath + "/StreamingAssets/" + filename;
        //    }

        //    string strPNG = PlayerPrefs.GetString("PNG");
        //    string[] str = strPNG.Split(',');
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        StartCoroutine(load(path, str[i]));
        //    }
        //}
        //IEnumerator load(string path, string name)
        //{
        //    WWW www = new WWW(path + "/" + name);
        //    yield return www;
        //    Texture2D t = www.texture;
        //    Sprite spr = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));

        //    Image im = Instantiate(Resources.Load("Image"), ImageParn) as Image;
        //    im.sprite = spr;
        //}
    }
}
