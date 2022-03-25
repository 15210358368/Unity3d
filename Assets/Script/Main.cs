using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    //配置读取
    LoadConfig loadConfig;
    Transform Points;
    Transform Role;

    // 读取到当前的配置
    List<string> list = new List<string>();
    Config cfg;
    SceneConfig scg;
    //是否开始
    bool update;

    // 显示的贴图
    public static string PointStr;
    public static string patternStr;

    // 赢的条件
    public string LinkCount;

    // 关卡配置
    List<Image> im = new List<Image>();
    int initID = 1;
    int number = 0;
    Dictionary<int, GameObject> Scene = new Dictionary<int, GameObject>();
    Dictionary<string, List<Transform>> dicObj = new Dictionary<string, List<Transform>>();
    public static Dictionary<int, List<Image>> EnterButton = new Dictionary<int, List<Image>>();

    bool isPlay;

    Transform canvas;

    void Start()
    {
        loadConfig = new LoadConfig();
        loadConfig.Init();

        // 获取到canvas
        //Points = GameObject.Find("Canvas").transform.FindChild("Points").transform;
        //Role = GameObject.Find("2DObject/Role").transform;

    }

    void OnLevelWasLoaded()
    {
        if (loadConfig == null)
        {
            loadConfig = new LoadConfig();
            loadConfig.Init();
        }
        canvas = GameObject.Find("Canvas").transform;
        isPlay = false;
        update = false;
        LoadSceneConfig();
        CreatePoints();
        StartCoroutine(UpdateTime());

    }
    void CreatePoints()
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject obj = Resources.Load("Points") as GameObject;
            Transform point = Instantiate(obj).transform;
            point.parent = canvas;
            point.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            point.GetComponent<RectTransform>().localPosition = Vector3.zero;
            point.name = list[i];
            if (!dicObj.ContainsKey(point.name))
            {
                List<Transform> listtran = new List<Transform>();
                foreach (Transform item in point)
                {
                    foreach (Transform tran in item)
                    {
                        tran.gameObject.AddComponent<SetControl>();
                        listtran.Add(tran.transform);
                    }
                    item.gameObject.SetActive(false);
                }
                dicObj.Add(point.name, listtran);
            }
        }
    }
    /// <summary>
    /// 停止背景播放
    /// </summary>
    /// <param name="target"></param>
    void StopEffect(bool target)
    {
        GameObject obj = GameObject.Find("GameObject");
        foreach (Transform item in obj.transform)
        {
            foreach (Transform tran in item)
            {
                Debug.Log("oooooooooooooo");
                if (tran.GetComponent<ParticleSystem>())
                {
                    if (target)
                    {
                        item.GetComponent<ParticleSystem>().Play();
                    }
                    else
                    {
                        item.GetComponent<ParticleSystem>().Stop();
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            Debug.Log(number + "_________OVER______________");
            IsOver();
            if (number == 9)
            {
                Debug.Log(number + "_______________________");
                isPlay = false;
                Remove(false);
                StopEffect(true);
                number = 0;
                StartCoroutine(UpdateTime());
            }
        }
    }
    /// <summary>
    /// 开启一个携程加载下一个关卡 如果下一个为结束 跳转场景
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateTime()
    {
        if (list[0] == "over")
        {
            Win();
            update = true;
        }
        yield return new WaitForSeconds(3f);
        if (!update)
        {
            StartGame();
        }
    }
    /// <summary>
    /// 判断 是不是连接成功
    /// </summary>
    void IsOver()
    {
        if (EnterButton.ContainsKey(1))
        {
            im = EnterButton[1];
            if (im.Count == scg.PointCount)
            {
                string[] str = LinkCount.Split(',');
                for (int i = 0; i < im.Count; i++)
                {
                    if (im[i].name != str[i])
                    {
                        im.Reverse();
                        Remove(true);
                        return;
                    }
                }
                number += 1;
            }
        }
    }

    /// <summary>
    /// 重置 背景图 
    /// </summary>
    void Remove(bool SetActive)
    {
        if (Points != null)
        {
            foreach (Transform item in Points)
            {
                foreach (Transform tran in item)
                {
                    tran.GetComponent<SetControl>().Exit();
                    //Texture2D t = null;
                    //Sprite temp = null;
                    //Image image = tran.GetComponent<Image>();
                    //if (tran.tag == "Point")
                    //{
                    //    image.color = new Color(255, 255, 255, 0);
                    //}
                    //else
                    //{
                    //    t = Resources.Load("icon/Item_Coin_01") as Texture2D;
                    //    temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
                    //}
                    //image.sprite = temp;
                }
                if (!SetActive)
                {
                    item.gameObject.SetActive(SetActive);
                }
            }
        }
        //if (!SetActive)
        //{
        EnterButton[999].Clear();
        EnterButton[1].Clear();
        //}
    }

    void StartGame()
    {
        if (!update)
        {
            scg = loadConfig.getSceneConfig(int.Parse(list[0]));
            LinkCount = scg.pointNumber;
            patternStr = scg.ShowEffect;
            Points = canvas.FindChild(list[0]).transform;
            foreach (Transform item in Points)
            {
                item.gameObject.SetActive(true);
            }
            isPlay = true;
            StopEffect(false);
            list.RemoveAt(0);
        }
    }


    void Win()
    {
        int initID = SceneManager.GetActiveScene().buildIndex + 1;
        if (initID <= 6)
        {
            SceneManager.LoadScene(initID);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
    void LoadSceneConfig()
    {
        list.Clear();
        foreach (var item in loadConfig.GetdicConfigs)
        {
            if (item.Key == SceneManager.GetActiveScene().buildIndex)
            {
                cfg = item.Value;
                string[] str = cfg.Image.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    list.Add(str[i]);
                }
                PointStr = item.Value.Backgroud;
            }
        }
        if (cfg.BG != "1")
        {
            GameObject obj1 = GameObject.Find("GameObject/_Level_Forrest_A/Fog_A");
            GameObject obj2 = GameObject.Find("GameObject/_Level_Forrest_A/Moon_B");
            obj1.SetActive(false);
            obj2.SetActive(false);
            GameObject obj3 = GameObject.Find("GameObject/_Level_Forrest_A/Back_Slime_A");
            Texture2D t = Resources.Load(cfg.BG) as Texture2D;
            Sprite spr = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));

            obj3.GetComponent<SpriteRenderer>().sprite = spr;
        }
    }
}
