using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum STATUS{
    OFFLINE,
    MASTERSERVER,
    JOINROOM,
    WAITING,
    PLAYING,
    RESULT,
    FINISHED
}
public class RoomManager : MonoBehaviourPunCallbacks
{

    public STATUS status{get;set;}
    public Text statusText;

    public Button titleButton;
    public Button leftButton;
    public Canvas disp;

    [System.NonSerialized]
    public GameObject player;




    private void Start() {
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();

        status = STATUS.OFFLINE;
        statusText.text = "LOADING...";


        //退出ボタン
        leftButton.onClick.AddListener(LeftRoom);
        leftButton.gameObject.SetActive(false);
        //タイトルボタン
        titleButton.onClick.AddListener(LeftRoom);
        titleButton.gameObject.SetActive(false);
    }


    private void Update() {
       if(status == STATUS.FINISHED){
           if(!titleButton.gameObject.activeSelf)
                titleButton.gameObject.SetActive(true);
       } 
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() {
        //ランダムな部屋に参加
        PhotonNetwork.JoinRandomRoom();       
        status = STATUS.MASTERSERVER;
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom() {
        Debug.Log("参加");

        leftButton.gameObject.SetActive(true);
        
        //管理用ネットワークオブジェクトの生成
        player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        player.name = "Player" + PhotonNetwork.LocalPlayer.ActorNumber;

        //ボタンを生成。ルームオブジェクトにする。マスター制御の生成。テキストの生成。
        var button = GameObject.Find("StartButton(Clone)");
        if(PhotonNetwork.IsMasterClient){
            if(button == null){
                var startButton = PhotonNetwork.InstantiateRoomObject("StartButton", Vector3.zero, Quaternion.identity);
                var PlayerList = PhotonNetwork.InstantiateRoomObject("PlayerList", Vector3.zero, Quaternion.identity);
                var questionObj = PhotonNetwork.InstantiateRoomObject("Question", Vector3.zero,Quaternion.identity);
                var kanaObj = PhotonNetwork.InstantiateRoomObject("Kana", Vector3.zero,Quaternion.identity); 
                var obj = PhotonNetwork.InstantiateRoomObject("Master",Vector3.zero, Quaternion.identity);
            }
        }

        
        status = STATUS.JOINROOM;
        statusText.text = "WAITING...";

        //人数maxなら打ち切り
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers) {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message){
        //入れる部屋がなかったら作る
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.PublishUserId = true;

        PhotonNetwork.CreateRoom(null, roomOptions);
        
    }

    public void LeftRoom(){
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Title");
    }

}
