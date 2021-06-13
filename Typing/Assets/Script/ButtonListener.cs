using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonListener : MonoBehaviour
{
    public Button soloButton;
    public Button multiButton;

    void Start()
    {
        soloButton.onClick.AddListener(SoloPlay);
        multiButton.onClick.AddListener(MultiPlay);
    }


    public void SoloPlay(){
        Debug.Log("Solo");
        SceneManager.LoadScene("SingleGame");
    }

    public void MultiPlay(){
        Debug.Log("Multi");
        SceneManager.LoadScene("MultiGame");
    }

}
