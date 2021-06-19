using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class StartButtonInit : MonoBehaviourPunCallbacks,NetworkObjectInit
{
    void Start() {
        ObjectInit();
    }

    public void ObjectInit(){
        var disp = GameObject.Find("Canvas").GetComponent<Canvas>();
        Debug.Log("startbutton");
        gameObject.name = "StartButton";
        gameObject.transform.SetParent(disp.transform);
        RectTransform rect = gameObject.transform as RectTransform;
        rect.localPosition = new Vector3(400, -300, 0);
        rect.localScale = new Vector3(1, 1, 1);
        Button button = gameObject.GetComponent<Button>();
        var player = GameObject.Find("Player"+PhotonNetwork.LocalPlayer.ActorNumber).GetComponent<GameManager>();
        button.onClick.AddListener(() => {player.PushStart(PhotonNetwork.CurrentRoom.PlayerCount);});
    }
}
