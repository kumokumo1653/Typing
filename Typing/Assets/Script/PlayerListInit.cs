using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerListInit : MonoBehaviourPunCallbacks,  NetworkObjectInit
{
    void Awake()
    {
        ObjectInit();
    }

    public void ObjectInit(){
        var disp = GameObject.Find("Canvas").GetComponent<Canvas>();
        var myOutput = gameObject.GetComponent<Text>();
        gameObject.transform.SetParent(disp.transform);
        RectTransform rect = gameObject.transform as RectTransform;
        rect.localPosition = new Vector3(0, -50, 0);
        rect.localScale = new Vector3(1, 1, 1);
    

    }
}
