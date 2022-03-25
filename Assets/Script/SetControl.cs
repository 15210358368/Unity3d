using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetControl : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
    public void Exit()
    {
        Image image = this.gameObject.GetComponent<Image>();
        Texture2D t = Resources.Load("icon/Item_Coin_01") as Texture2D;
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
        image.sprite = temp;
    }
    /// <summary>
    /// 经过是触发的函数
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.tag == "Point")
        {

            // g更换贴图 并添加到dic 中
            Image image = this.gameObject.GetComponent<Image>();
            Texture2D t = Resources.Load("icon/"+ Main.PointStr) as Texture2D;
            Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
            image.color = new Color(255,255,255,255);
            image.sprite = temp;
            if (Main.EnterButton.ContainsKey(999))
            {
                List<Image> list = Main.EnterButton[999];
                if (!list.Contains(image))
                {
                    list.Add(image);
                }
            }
            else
            {
                List<Image> list = new List<Image>();
                list.Add(image);
                Main.EnterButton.Add(999, list);
            }
        }
        else
        {
            Debug.Log("============"+ Main.patternStr);
            Image image = this.gameObject.GetComponent<Image>();
            Texture2D t = Resources.Load("icon/"+ Main.patternStr) as Texture2D;
            Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
            image.sprite = temp;

            if (Main.EnterButton.ContainsKey(1))
            {
                List<Image> list = Main.EnterButton[1];
                if (!list.Contains(image))
                {
                    list.Add(image);
                }
            }
            else
            {
                List<Image> list = new List<Image>();
                list.Add(image);
                Main.EnterButton.Add(1, list);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
