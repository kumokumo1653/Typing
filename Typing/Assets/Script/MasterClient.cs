using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MasterClient : MonoBehaviourPunCallbacks,IPunInstantiateMagicCallback
{

    private Text Question;
    private Text kana;

    public int postNumber;

    private int postedNumber = 0;

    private int[] postedQuestions;

    

    void Start()
    {
        Question = GameObject.Find("Question").GetComponent<Text>();
        kana = GameObject.Find("Kana").GetComponent<Text>();
    }


    public void GameInit(){
        //問題の設定
        int[] orderArray = Enumerable.Range(0,QuestionCollection.questions.GetLength(0)).ToArray();
        postedQuestions = orderArray.OrderBy(i => System.Guid.NewGuid()).ToArray();


    }

    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info) {
        if(info.photonView.gameObject.name == "Master(Clone)"){
            info.photonView.gameObject.name = "Master";
        }
    }

    public bool NextQuestion(out int question){
        if(postedNumber == postedQuestions.Length){
            Debug.Log("finish");
            question = -1;
            return false;
        }else{
            postedNumber++;
            question = postedQuestions[postedNumber - 1];
            return true;
        }
    }

    public bool CheckContinue(){
        var players = PhotonNetwork.PlayerList;
        int cnt;
        for(int i = 0; i < players.Length;i++){
            if(players[i].GetWinCount(out cnt)){
                Debug.Log(players[i].NickName + players[i].ActorNumber + ":" + cnt);
                if(cnt >= postNumber) 
                    return false;
            }
        }
        return true;
    }

}
