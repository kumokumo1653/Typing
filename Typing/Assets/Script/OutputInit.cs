using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class OutputInit : MonoBehaviourPunCallbacks,IPunInstantiateMagicCallback,NetworkObjectInit
{

    private float elapsedTime;
    private bool preF;
    private OutputText output;
    void Awake() {
        ObjectInit();
        elapsedTime = 0;
        output = this.gameObject.GetComponent<OutputText>();
    }
    void Start() {
        preF = true;
    }
    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info) {
        if(info.photonView.gameObject.name == "Output(Clone)"){
            info.photonView.gameObject.name = "Output" + info.photonView.OwnerActorNr;
        }
    }


    void Update() {
        elapsedTime += Time.deltaTime;
        //100msごとにチェック
        if(elapsedTime > 0.1f){
            elapsedTime = 0f;
            //打ち終わったら
            if(preF != output.typingF && !output.typingF){
                preF = false;
                photonView.RPC(nameof(SendFinish), RpcTarget.All);
            }
            if(preF != output.typingF && output.typingF) preF = true;


        }

    }

    [PunRPC]
    public void SendFinish(PhotonMessageInfo info){
        bool f;
        int cnt;
        if(PhotonNetwork.CurrentRoom.GetFinishF(out f)){
            //先着のみ処理をする
            if(f) return;
            PhotonNetwork.CurrentRoom.SetFinishF(true);
            if(info.Sender.GetWinCount(out cnt)){
                info.Sender.SetWinCount(cnt + 1);
            }


        }
    }


    public void ObjectInit(){
        var disp = GameObject.Find("Canvas").GetComponent<Canvas>();
        var myOutput = gameObject.GetComponent<Text>();
        gameObject.transform.SetParent(disp.transform);
        RectTransform rect = gameObject.transform as RectTransform;
        rect.localPosition = new Vector3(0, 50, 0);
        rect.localScale = new Vector3(1, 1, 1);
        myOutput.text = gameObject.name;
        var outputText = gameObject.GetComponent<OutputText>();
        var question = GameObject.Find("Question").GetComponent<Text>();
        var kana = GameObject.Find("Kana").GetComponent<Text>();
        outputText.typingF = false;
        outputText.text = question;
        outputText.kana = kana;
        outputText.output = this.gameObject.GetComponent<Text>();
        this.gameObject.GetComponent<Text>().fontSize = 36;
    

    }

}
