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
    PLAYING,
}
public class RoomManager : MonoBehaviourPunCallbacks
{

    public STATUS status{get;set;}
    public Text statusText;
    public Text playersText;

    public Button LeftButton;
    public Canvas disp;

    [System.NonSerialized]
    public GameObject player;



    private float elapsedTime;

    private void Start() {
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();

        status = STATUS.OFFLINE;
        statusText.text = "LOADING...";

        elapsedTime = 0f;

        //退出ボタン
        LeftButton.onClick.AddListener(LeftRoom);
        LeftButton.gameObject.SetActive(false);
    }


    private void Update() {
        
        if(status == STATUS.JOINROOM){
            elapsedTime += Time.deltaTime;
            if(elapsedTime > 0.15f){
                elapsedTime = 0f;
                var players = PhotonNetwork.PlayerList;
                playersText.text = "";
                for(int i = 0; i < players.Length;i++){
                    playersText.text += i + 1 + ": " + players[i].NickName + players[i].ActorNumber + "\n";
                }
            }
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
        //管理用ネットワークオブジェクトの生成
        player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        player.name = "Player" + PhotonNetwork.LocalPlayer.ActorNumber;
        //ボタンを生成。ルームオブジェクトにする。マスター制御の生成。
        if(PhotonNetwork.IsMasterClient){
            var startButton = PhotonNetwork.InstantiateRoomObject("StartButton", Vector3.zero, Quaternion.identity);
            startButton.name = "StartButton";
            startButton.transform.SetParent(disp.transform);
            RectTransform rect = startButton.transform as RectTransform;
            rect.localPosition = new Vector3(400, -300, 0);
            rect.localScale = new Vector3(1, 1, 1);
            Button button = startButton.GetComponent<Button>();
            button.onClick.AddListener(() => {player.GetComponent<GameManager>().PushStart(PhotonNetwork.CurrentRoom.PlayerCount);});


            var obj = PhotonNetwork.InstantiateRoomObject("Master",Vector3.zero, Quaternion.identity);
            obj.name = "Master";
        }

        LeftButton.gameObject.SetActive(true);
        
        status = STATUS.JOINROOM;
        statusText.text = "WAITING...";

        //人数maxなら打ち切り
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers) {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            //ボタンを押したことにする。
            player.GetComponent<GameManager>().PushStart(PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message){
        //入れる部屋がなかったら作る
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(null, roomOptions);
        
    }

    public void LeftRoom(){
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Title");
    }


}
