using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update

    string kana = "あいう、";
    void Start()
    {
        for(int i = 0; i < kana.Length; i++){
            switch (kana[i])
            {
                case 'あ': Debug.Log("a");break;
                case 'い': Debug.Log("i");break;
                case 'う': Debug.Log("u");break; 
                case '、': Debug.Log(",");break; 
                default:break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
