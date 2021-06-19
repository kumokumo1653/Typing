using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class KanaInit : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback, NetworkObjectInit
{
    void Awake() {
       ObjectInit(); 
    }

    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info) {
        if(info.photonView.gameObject.name == "Kana(Clone)"){
            info.photonView.gameObject.name = "Kana";
        }
    }


    public void ObjectInit(){
        var disp = GameObject.Find("Canvas").GetComponent<Canvas>();
        var kana = gameObject.GetComponent<Text>();
        gameObject.transform.SetParent(disp.transform);
        RectTransform rect = gameObject.transform as RectTransform;
        rect.localPosition = new Vector3(0, 150, 0);
        rect.localScale = new Vector3(1, 1, 1);
    }
}
