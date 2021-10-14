using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickHelp : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text help;

    private bool click;

    void Start()
    {
        click = false;
        this.gameObject.GetComponent<Button>().onClick.AddListener(helpclick);               
    }

    public void helpclick(){
        click = !click;
        if(click){
            help.enabled = true;
            panel.GetComponent<Image>().enabled = true;
            this.gameObject.transform.Find("Text").GetComponent<Text>().text = "もどる";
        }else{
            help.enabled = false;
            panel.GetComponent<Image>().enabled = false;
            this.gameObject.transform.Find("Text").GetComponent<Text>().text = "ヘルプ";
        }
    }
}
