using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class StartButtonTransform : MonoBehaviourPunCallbacks, IPunObservable
{
    private RectTransform rect;

    void Awake()
    {
        rect = this.gameObject.GetComponent<RectTransform>();
    }
    
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // Transformの値をストリームに書き込んで送信する
            stream.SendNext(rect.localPosition);
            stream.SendNext(rect.localRotation);
            stream.SendNext(rect.localScale);
            stream.SendNext(this.gameObject.transform.parent);
            
        } else {
            // 受信したストリームを読み込んでTransformの値を更新する
            rect.localPosition = (Vector3)stream.ReceiveNext();
            rect.localRotation = (Quaternion)stream.ReceiveNext();
            rect.localScale = (Vector3)stream.ReceiveNext();
            this.gameObject.transform.parent = (Transform)stream.ReceiveNext();
        }
    }
}
