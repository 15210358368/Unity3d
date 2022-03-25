using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class open : MonoBehaviour
{
    Transform canvas;
    Transform qiut;
    Transform openbut;
    Transform closebut;
    Transform openImage;
    // Use this for initialization
    void Start()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        openbut = canvas.transform.Find("openImage");

        openbut.GetComponent<Button>().onClick.AddListener(delegate { openimage(); });
        closebut = canvas.transform.Find("quitImage");
        closebut.GetComponent<Button>().onClick.AddListener(delegate { close(); });
        openImage = canvas.transform.Find("Images");
        openImage.gameObject.SetActive(false);
    }


    // Update is called once per frame

    void close()
    {
        openImage.transform.gameObject.SetActive(false);
        closebut.gameObject.SetActive(false);
    }
    void openimage()
    {
        openImage.transform.gameObject.SetActive(true);
        closebut.gameObject.SetActive(true);
    }

    void Update()
    {

    }
}
