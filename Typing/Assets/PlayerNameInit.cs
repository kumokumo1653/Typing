using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerNameInit : MonoBehaviourPunCallbacks,NetworkObjectInit
{
     private RoomManager room;
    void Awake()
    {
        room = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        ObjectInit();
    }

    // Update is called once per frame
    void Update()
    {
        if(room.status == STATUS.FINISHED){
            this.gameObject.SetActive(false);
        }    
    }

    public void ObjectInit(){

        gameObject.name = "PlayerName" + photonView.OwnerActorNr;
        var disp = GameObject.Find("Canvas").GetComponent<Canvas>();
        gameObject.transform.SetParent(disp.transform);
        RectTransform rect = gameObject.transform as RectTransform;
        gameObject.GetComponent<Text>().text = "Player" + photonView.OwnerActorNr;
        rect.localPosition = new Vector3(-500, -240 - (photonView.OwnerActorNr - 1) * 30, 0);
        rect.localScale = new Vector3(1, 1, 1); 
    }
    // Start is called before the first frame update
}
