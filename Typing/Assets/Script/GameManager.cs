using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
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

    private OutputText myOutput;
    private GameObject myOutputObj;

    private Canvas disp;


    private Text countText;

    private Text playersText;

    private Dictionary<string, int> ranking;
    private string winner;

    private float elapsedTime;
    private float waitTime;

    //sound

    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioClip downClip;
    [SerializeField]
    private AudioClip lastClip;

    private bool done = false;

    private GameObject localBar;
    
    void Awake() {
        statusText = GameObject.Find("statusText").GetComponent<Text>();
        room = GameObject.Find("RoomManager").GetComponent<RoomManager>();

        disp = GameObject.Find("Canvas").GetComponent<Canvas>();
        
        countText = GameObject.Find("Count").GetComponent<Text>();
        leftButton = GameObject.Find("LeftButton").GetComponent<Button>();
        elapsedTime = 0;
        waitTime = 0;
        ranking = new Dictionary<string, int>();
        if(audio == null)
            audio = gameObject.AddComponent<AudioSource>();

    }

    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
            playersText = GameObject.Find("PlayerList(Clone)").GetComponent<Text>();
    }
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > 0.1f){
            elapsedTime = 0f;
        
            if(PhotonNetwork.IsMasterClient){
                if(room.status == STATUS.JOINROOM){
                    var players = PhotonNetwork.PlayerList;
                    playersText.text = "";
                    for(int i = 0; i < players.Length;i++){
                        playersText.text += i + 1 + ": " + players[i].NickName + players[i].ActorNumber + "\n";
                    }
                }
                if(room.status == STATUS.PLAYING && myOutput != null){
                    bool flag;
                    if(PhotonNetwork.CurrentRoom.GetFinishF(out flag)){
                        if(flag && !done){
                            done = true;
                            int num;
                            if(master.NextQuestion(out num) && master.CheckContinue() ){
                                Debug.Log("NextQuestion!!!!!!!");
                                    //roomのflag　　falseに
                                    PhotonNetwork.CurrentRoom.SetFinishF(false);
                                    //rpc output問題の設定
                                    myOutput.typingF = true;
                                    photonView.RPC(nameof(SetQuestion), RpcTarget.AllViaServer, num);
                            }else{
                                Debug.Log("終了");
                                GameObject.Find("Question").GetComponent<Text>().text = ""; 
                                GameObject.Find("Kana").GetComponent<Text>().text = ""; 
                                photonView.RPC(nameof(SendResult), RpcTarget.All);
                                
                                room.status = STATUS.RESULT;

                            }
                        }
                    }
                }

                if(room.status == STATUS.RESULT){
                    var sortedrank = ranking.OrderByDescending((a) => a.Value);
                    Debug.Log("result");
                    playersText.text = "<size=48>" + winner + "WIN</size>\n";
                    foreach(var player in sortedrank){
                        Debug.Log(player.Key + ":" + player.Value + "pt");
                        playersText.text += player.Key + ":" + player.Value + "pt" + "\n";
                    }
                    room.status = STATUS.FINISHED;
                }
                if(room.status == STATUS.FINISHED){
                }
            }
        }
    }


    [PunRPC]
    public void SendResult(){
        myOutputObj.GetComponent<Text>().text = "";
        if(!PhotonNetwork.IsMasterClient)
            room.status = STATUS.FINISHED;
        var players = PhotonNetwork.PlayerList;
        int cnt;
        int max = 0;
        for(int i = 0; i < players.Length;i++){
            if(players[i].GetWinCount(out cnt)){
                ranking.Add(players[i].NickName + players[i].ActorNumber, cnt);
                if(max < cnt){
                    max = cnt;
                    winner = players[i].NickName + players[i].ActorNumber;
                }
            }
        }
        foreach(var player in ranking){
            Debug.Log(player.Key + ":" + player.Value + "pt");
        }
    }
    [PunRPC]
    public void SetQuestion(int num,PhotonMessageInfo info){
        Debug.Log("Question:"+ num);
        Debug.Log(info.Sender.NickName + info.Sender.ActorNumber);
        myOutput.q = new Question(QuestionCollection.questions[num, 0], QuestionCollection.questions[num, 1]);
        myOutput.QuestionInit();
        done = false;
    }
    [PunRPC]
    public void enter(){
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        if(room.status == STATUS.WAITING)return;

        room.status = STATUS.WAITING;

        startButton.gameObject.SetActive(false);
        leftButton.gameObject.SetActive(false);
        statusText.text = "";

        //獲得本数の初期化
        PhotonNetwork.LocalPlayer.SetWinCount(0);

        master = GameObject.Find("Master").GetComponent<MasterClient>();
        if(PhotonNetwork.IsMasterClient){
            playersText.text = "";
            master.GameInit();
        }

        
        MyOutputInit();

        //processBar

        localBar = PhotonNetwork.Instantiate("Slider", new Vector3(0,0,0), Quaternion.identity);
        PhotonNetwork.Instantiate("PlayerName", Vector3.zero, Quaternion.identity);
    }

    public void PushStart(int number){
        if(number == 1) return;
        if(photonView.IsMine){ 
            photonView.RPC( nameof(enter), RpcTarget.AllViaServer);
        }
        
    }

    private void MyOutputInit(){
        Debug.Log("instanse");
        myOutputObj = PhotonNetwork.Instantiate("Output", Vector3.zero, Quaternion.identity);
        myOutput = myOutputObj.GetComponent<OutputText>();

        //他のプレイヤーに位置調整の関数を呼び出させる。
        photonView.RPC( nameof(MoveOutput), RpcTarget.Others,myOutputObj.name);
    }

    [PunRPC]
    private void MoveOutput(string objName){
        Debug.Log(objName);
        Debug.Log(master);
        var players = PhotonNetwork.PlayerListOthers;
        GameObject output = GameObject.Find(objName);
        if(output != null){
            for(int i = 0; i < players.Length;i++){
                if(output.GetComponent<PhotonView>().OwnerActorNr == players[i].ActorNumber){
                    RectTransform rect = output.transform as RectTransform;
                    rect.localPosition = new Vector3(0, -50 + i * -50, 0);
                    output.GetComponent<Text>().fontSize = 20;
                    output.GetComponent<Text>().text = players[i].ActorNumber + "," + i;
                }
            }
        }

        StartCoroutine("CountDown");
    }

    private IEnumerator CountDown() {
        countText.text = "3";
        audio.PlayOneShot(downClip, 1.0f);
        yield return new WaitForSeconds(1.0f);
        countText.text = "2";
        audio.PlayOneShot(downClip, 1.0f);
        yield return new WaitForSeconds(1.0f);
        countText.text = "1";
        audio.PlayOneShot(downClip, 1.0f);
        yield return new WaitForSeconds(1.0f);
        countText.text = "";
        audio.PlayOneShot(lastClip, 1.0f);

        room.status = STATUS.PLAYING;
        //開始時間の保存
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
            PhotonNetwork.CurrentRoom.SetFinishF(true);
        }
        int a;
        Debug.Log(PhotonNetwork.LocalPlayer.GetWinCount( out a));
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + PhotonNetwork.LocalPlayer.ActorNumber);
        yield break;
    }
    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info) {
        if(info.photonView.gameObject.name == "Player(Clone)"){
            info.photonView.gameObject.name = "Player" + info.photonView.OwnerActorNr;
        }
    }



}
