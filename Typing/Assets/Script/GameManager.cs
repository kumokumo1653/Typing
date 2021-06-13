using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    private Text statusText;
    private Button startButton;
    void Awake() {
        Debug.Log("start");
        statusText = GameObject.Find("statusText").GetComponent<Text>();
    }
    [PunRPC]
    public void enter(){
        Debug.Log("Enter");
        startButton.interactable = false;
        statusText.text = "PLAYING!!";
    }

    public void PushStart(int number){
        if(number == 1) return;
        
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        Debug.Log(startButton);
        photonView.RPC( nameof(enter), RpcTarget.All);
        
    }
}
