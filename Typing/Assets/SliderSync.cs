using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SliderSync : MonoBehaviourPunCallbacks, IPunObservable
{
    private Slider slider;

    void Awake()
    {
        slider = this.gameObject.GetComponent<Slider>();
    }

    void Start()
    {
    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

        if(stream.IsWriting){
            stream.SendNext(slider.value);
        }else{
            slider.value = (float)stream.ReceiveNext();
        }
    }
}
