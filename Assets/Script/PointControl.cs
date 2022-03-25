using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PointControl : MonoBehaviour
{
    List<Image> changePointBtn = new List<Image>();

    public static Dictionary<int, List<Image>> EnterButton = new Dictionary<int, List<Image>>();
    // 连接点的集合
    Transform LinkPoint;
    Transform Point;
    Transform Points;


    void Start()
    {
        Transform tran = GameObject.Find("Canvas").transform;
        Points = tran.Find("Points");
        LinkPoint = tran.Find("Points/LinkPoint");
        Point = tran.Find("Points/Point");
        SetCilck();
    }
    /// <summary>
    /// 设置单机事件
    /// </summary>
    private void SetCilck()
    {
        foreach (Transform item in Point)
        {
            item.gameObject.AddComponent<SetControl>();
        }
        foreach (Transform item in LinkPoint)
        {
            item.gameObject.AddComponent<SetControl>();
        }
        foreach (Transform item in Points)
        {
            foreach (Transform tran in item)
            {
                changePointBtn.Add(tran.GetComponent<Image>());
            }
        }
    }
    /// <summary>
    /// 根据ID 清空现在当前dic 中的物体并还原设置
    /// </summary>
    /// <param name="resetID"></param>
    public void resetPoint()
    {
        for (int i = 0; i < changePointBtn.Count; i++)
        {
            Texture2D t = null;
            Sprite temp = null;
            Image image = changePointBtn[i].GetComponent<Image>();
            if (changePointBtn[i].tag == "Point")
            {
                image.color = new Color(255, 255, 255, 0);
            }
            else
            {
                t = Resources.Load("icon/Item_Coin_01") as Texture2D;
                temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
            }
            image.sprite = temp;
        }

        EnterButton.Clear();

    }
    void Update()
    {

    }
}
