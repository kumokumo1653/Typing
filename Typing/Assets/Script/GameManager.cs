using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks,IPunInstantiateMagicCallback
{

    private Text statusText;
    private Button startButton;

    private Button leftButton;

    private RoomManager room;
    private MasterClient master;

    private Text myOutput;

    private Canvas disp;

    
    void Awake() {
        statusText = GameObject.Find("statusText").GetComponent<Text>();
        //leftButton = GameObject.Find("LeftButton").GetComponent<Button>();
        room = GameObject.Find("RoomManager").GetComponent<RoomManager>();

        disp = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    [PunRPC]
    public void enter(){
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        if(room.status == STATUS.PLAYING)return;

        room.status = STATUS.PLAYING;
        statusText.text = "PLAYING!!";

        startButton.gameObject.SetActive(false);
        //leftButton.gameObject.SetActive(false);


        master = GameObject.Find("Master(Clone)").GetComponent<MasterClient>();
        if(PhotonNetwork.IsMasterClient){
            master.GameInit();
        }
        MyOutputInit();


    }

    public void PushStart(int number){
        if(number == 1) return;
        if(photonView.IsMine){ 
            photonView.RPC( nameof(enter), RpcTarget.AllViaServer);
        }
        
    }

    private void MyOutputInit(){
        Debug.Log("instanse");
        var obj = PhotonNetwork.Instantiate("Output", Vector3.zero, Quaternion.identity);

        //他のプレイヤーに位置調整の関数を呼び出させる。
        photonView.RPC( nameof(MoveOutput), RpcTarget.Others,obj.name);
    }

    [PunRPC]
    private void MoveOutput(string objName){
        Debug.Log(objName);
        Debug.Log(master);
        var players = PhotonNetwork.PlayerListOthers;
        GameObject output = GameObject.Find(objName);
        Debug.Log("呼び出し元:"+output.GetComponent<PhotonView>().OwnerActorNr);
        Debug.Log("実行元:" + PhotonNetwork.LocalPlayer.ActorNumber);
        if(output != null){
            for(int i = 0; i < players.Length;i++){
                Debug.Log("リスト"+i+":" + players[i].ActorNumber);
                if(output.GetComponent<PhotonView>().OwnerActorNr == players[i].ActorNumber){
                    RectTransform rect = output.transform as RectTransform;
                    rect.localPosition = new Vector3(0, i * -50, 0);
                    output.GetComponent<Text>().text = players[i].ActorNumber + "," + i;
                }
            }
        }
    }

    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info) {
        if(info.photonView.gameObject.name == "Player(Clone)"){
            info.photonView.gameObject.name = "Player" + info.photonView.OwnerActorNr;
        }
    }



}
