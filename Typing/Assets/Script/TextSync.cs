using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TextSync : MonoBehaviourPunCallbacks, IPunObservable
{

    private Text text;

    void Awake()
    {
        text = this.gameObject.GetComponent<Text>();
    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

        if(stream.IsWriting){
            stream.SendNext(text.text);
        }else{
            text.text = (string)stream.ReceiveNext();
        }
    }
}
