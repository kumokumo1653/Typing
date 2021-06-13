using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class AvatarController : MonoBehaviourPunCallbacks
{
    private void Update() {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine) {
            var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            transform.Translate(6f * Time.deltaTime * input.normalized);
        }
    }
}