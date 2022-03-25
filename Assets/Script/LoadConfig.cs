using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.Collections;
using System.IO;

public class Config
{
    public int ID; // -->  第几关
    public string Name;// -- > 关卡名字
    public string Backgroud;
    public string Image;
    public string BG;
}
public class SceneConfig
{
    public int ID;
    public int PointCount; // -- > 连接点的长度
    public string pointNumber;// -- > 连接的点
    public string ShowEffect;
    public int SceneID;
}

public class LoadConfig
{
    Dictionary<int, Config> dicConfig = new Dictionary<int, Config>();
    Dictionary<int, SceneConfig> dicSceneConfig = new Dictionary<int, SceneConfig>();


    public void Init()
    {
        LoadXML<Config>(dicConfig, "Config");
        LoadXML<SceneConfig>(dicSceneConfig, "Level");
    }

    public Dictionary<int, Config> GetdicConfigs
    {
        get
        {
            return dicConfig;
        }
    }
    public SceneConfig getSceneConfig(int ID )
    {
        SceneConfig sc = new SceneConfig();
        foreach (var item in dicSceneConfig)
        {
            if (item.Key == ID)
            {
                sc = item.Value;
            }
        }
        return sc;
    }

    private void LoadXML<T>(Dictionary<int, T> dic, string tablename)
    {

        TextAsset text = Resources.Load("Config/" + tablename) as TextAsset;

        MemoryStream memorysteam = new MemoryStream(text.bytes);
        StreamReader streamreader = new StreamReader(memorysteam, System.Text.Encoding.UTF8);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(streamreader.ReadToEnd());

        XmlNodeList node = doc.SelectNodes("Nodes/Node");

        if (node != null)
        {
            for (int i = 0; i < node.Count; i++)
            {
                XmlElement elem = node[i] as XmlElement;

                T obj = XmlHelper.GreateAndSetValue<T>(elem);

                int ID = (int)obj.GetType().GetField("ID").GetValue(obj);

                if (!dic.ContainsKey(ID))
                {
                    dic.Add(ID, obj);
                }
            }
        }
    }
}

