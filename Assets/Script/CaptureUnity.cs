using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CaptureUnity : MonoBehaviour
{
    //定义路径
    private string mPath1;
    //保存按钮
    public Button save;
    public Text text;
    int i = 0;
    void Start()
    {
        ScreenshotManager.ScreenshotFinishedSaving += ShowText2;

        //保存
        save.onClick.AddListener(delegate ()
        {
            //StartCoroutine(ScreenshotManager.Save("MyScreenshot", "MyApp", true));
            i++;
            //判断平台
            if (Application.platform == RuntimePlatform.Android)
            {
                //定义图片名字
                string filename = "涂鸦" + i + ".png";
                //定义保存路径
                string destination = "/sdcard/DCIM/Camera";
                //判断目录是否存在，不存在则会创建目录  
                if (!System.IO.Directory.Exists(destination))
                {
                    Directory.CreateDirectory(destination);
                }
                mPath1 = destination + "/" + filename;
                //保存图片
                StartCoroutine(CaptureByRect(new Rect(290, 155, 550, 460), mPath1));
            }
            else
            {
                //定义图片名字
                string filename = "//涂鸦" + i + ".png";
                //定义保存路径
                mPath1 = Application.dataPath + "/StreamingAssets/" + filename;
                //保存图片
                StartCoroutine(CaptureByRect(new Rect(332, 183, 554.5f, 464.5f), mPath1));
                
                string str = PlayerPrefs.GetString("PNG");
                str = str + filename + ",";
                PlayerPrefs.SetString("PNG", str);
            }
        });
    }
    public void ShowText2()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        text.gameObject.SetActive(false);
    }
    /// <summary>  
    /// 根据一个Rect类型来截取指定范围的屏幕  
    /// 左下角为(0,0)  
    /// </summary>  
    /// <param name="mRect">M rect.</param>  
    /// <param name="mFileName">M file name.</param>  
    public IEnumerator CaptureByRect(Rect mRect, string mFileName)
    {
        //等待渲染线程结束  
        yield return new WaitForEndOfFrame();
        //初始化Texture2D  
        Texture2D mTexture = new Texture2D((int)mRect.width, (int)mRect.height, TextureFormat.RGB24, false);
        //读取屏幕像素信息并存储为纹理数据  
        mTexture.ReadPixels(mRect, 0, 0);
        //应用  
        mTexture.Apply();
        //将图片信息编码为字节信息  
        byte[] bytes = mTexture.EncodeToPNG();
        //保存  
        System.IO.File.WriteAllBytes(mFileName, bytes);
    }
}
