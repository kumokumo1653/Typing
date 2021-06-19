using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class QuestionInit : MonoBehaviour,IPunInstantiateMagicCallback, NetworkObjectInit {
    void Awake() {
       ObjectInit(); 
    }

    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info) {
        if(info.photonView.gameObject.name == "Question(Clone)"){
            info.photonView.gameObject.name = "Question";
        }
    }


    public void ObjectInit(){
        var disp = GameObject.Find("Canvas").GetComponent<Canvas>();
        var kana = gameObject.GetComponent<Text>();
        gameObject.transform.SetParent(disp.transform);
        RectTransform rect = gameObject.transform as RectTransform;
        rect.localPosition = new Vector3(0, 200, 0);
        rect.localScale = new Vector3(1, 1, 1);
    }
}
