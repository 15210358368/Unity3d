using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavePNG : MonoBehaviour
{
    public Text text;
     Transform showimage;
    List<Image> list = new List<Image>();

    bool BOLIMGAE;
    int i = 0;
    void Start()
    {
        GameObject gam = GameObject.Find("Canvas");
        showimage = gam.transform.Find("Images/kongjian");
        foreach (Transform item in showimage)
        {
            Image image = item.GetComponent<Image>();
            list.Add(image);
            image.gameObject.SetActive(false);
        }
        BOLIMGAE = false;
    }


    public void Cilck()
    {
        i += 1;
        BOLIMGAE = true;
        StartCoroutine(GetCapture());
    }
    /// <summary>
    /// 开始一个协成，对屏幕当前屏幕画面截图
    /// </summary>
    /// <returns></returns>
    IEnumerator GetCapture()
    {
        //等待渲染线程结束  
        yield return new WaitForEndOfFrame();

        //获取当前的屏幕尺寸
        int width = Screen.width;
        int height = Screen.height;

        //Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        //tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);

        //byte[] imagebytes = tex.EncodeToPNG();//转化为png图

        //tex.Compress(false);//对屏幕缓存进行压缩

        //初始化Texture2D  
        Texture2D mTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
        //读取屏幕像素信息并存储为纹理数据  
        mTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        //应用  
        list[0].material.mainTexture = mTexture;
        list[0].gameObject.SetActive(true);
        list.Remove(list[0]);

        ShowText2();

        mTexture.Apply();
        //将图片信息编码为字节信息  
        byte[] bytes = mTexture.EncodeToPNG();
        //保存  
        string Path;
        string paht1;
        string filename = null;
        if (Application.platform == RuntimePlatform.Android)
        {
            //定义图片名字
            filename = "涂鸦" + i + ".png";
            //定义保存路径
            paht1 = "/sdcard/DCIM/Camera";
            //判断目录是否存在，不存在则会创建目录  
            if (!System.IO.Directory.Exists(paht1))
            {
                Directory.CreateDirectory(paht1);
            }
            Path = paht1 + "/" + filename;
        }
        else
        {
            //定义图片名字
            filename = "/涂鸦" + i + ".png";
            //定义保存路径
            paht1 = Application.dataPath;
            Path = paht1 + filename;
        }

        System.IO.File.WriteAllBytes(Path, bytes);
        //list[0].material.mainTexture = mTexture;
        //list[0].gameObject.SetActive(true);
        //list.Remove(list[0]);

        //ShowText2();

        //string path = null;
        //string filename = "PNG";
        //// 选择路径
        ////Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer ||
        //if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        //{
        //    path = Application.dataPath + "/StreamingAssets/" + filename;
        //}
        ////else if (Application.platform == RuntimePlatform.IPhonePlayer)
        ////{
        ////    path = Application.dataPath + "/Raw/" + filename;
        ////}
        //else if (Application.platform == RuntimePlatform.Android)
        //{
        //    //path = "jar:file://" + Application.dataPath + "!/assets/" + filename;
        //    path = "/sdcard/DCIM/Camera";
        //}
        //else
        //{
        //    Debug.Log("Error : 添加文件平台");
        //    //path = Application.dataPath + "/config/" + filename;
        //}
        //if (!Directory.Exists(path))
        //{
        //    Directory.CreateDirectory(path);  //创建文件夹 重名会加1出现
        //}
        //string PNGname = "PNG " + Time.deltaTime.ToString();


        //File.WriteAllBytes(path + "/" + PNGname + ".png", imagebytes);//存储png图

        //ShowText2();
        //Debug.Log(string.Format("截屏了一张照片: {0}", path + "/ " + PNGname + ".png"));

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
    Image Change(bool boos)
    {
        Image IM = null;
        if (boos)
        {

            for (int i = 0; i < list.Count; i++)
            {
                list[0].gameObject.SetActive(true);
                list.RemoveAt(0);
                IM = list[0];
                boos = false;
                return IM;
            }
        }
        return IM;

    }
}
