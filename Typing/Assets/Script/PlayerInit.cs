using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInit : MonoBehaviourPunCallbacks
{
    void Start() {
        gameObject.name = "Player" + PhotonNetwork.LocalPlayer.ActorNumber;
        
    }

    void Update()
    {
        
    }
}
