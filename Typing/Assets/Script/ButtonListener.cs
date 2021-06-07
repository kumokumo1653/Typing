using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonListener : MonoBehaviour
{
    public Button soloButton;

    void Start()
    {
        soloButton.onClick.AddListener(SoloPlay);
    }


    public void SoloPlay(){
        Debug.Log("Solo");
        SceneManager.LoadScene("SingleGame");
    }


}
