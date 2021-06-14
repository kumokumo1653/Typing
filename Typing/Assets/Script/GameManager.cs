using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    private Text statusText;
    private Button startButton;

    private Button leftButton;

    private RoomManager room;
    private MasterClient master;

    private Text myOutput;

    private Canvas disp;
    void Awake() {
        Debug.Log("start");
        statusText = GameObject.Find("statusText").GetComponent<Text>();
        //leftButton = GameObject.Find("LeftButton").GetComponent<Button>();
        room = GameObject.Find("RoomManager").GetComponent<RoomManager>();

        disp = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    [PunRPC]
    public void enter(){
        Debug.Log("Enter");
        if(room.status == STATUS.PLAYING)return;

        room.status = STATUS.PLAYING;
        statusText.text = "PLAYING!!";

        startButton.gameObject.SetActive(false);
        //leftButton.gameObject.SetActive(false);


        master = GameObject.Find("Master").GetComponent<MasterClient>();
        if(PhotonNetwork.IsMasterClient){
            master.GameInit();
        }
        MyOutputInit();


    }

    public void PushStart(int number){
        //if(number == 1) return;
        if(photonView.IsMine){ 
            startButton = GameObject.Find("StartButton").GetComponent<Button>();
            photonView.RPC( nameof(enter), RpcTarget.AllViaServer);
        }
        
    }

    private void MyOutputInit(){
        if(photonView.IsMine){
            var obj = PhotonNetwork.Instantiate("Output", Vector3.zero, Quaternion.identity);
            myOutput = obj.GetComponent<Text>();
            obj.name = "Output" + photonView.OwnerActorNr;
            obj.transform.SetParent(disp.transform);
            RectTransform rect = obj.transform as RectTransform;
            rect.localPosition = new Vector3(0, 0, 0);
            rect.localScale = new Vector3(1, 1, 1);
            myOutput.text = obj.name;

        }
    }

    private void MoveOutput(){
        
    }
}
