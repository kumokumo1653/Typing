using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    Question q = new Question("あいうえお", "っっ");

    void Start()
    {
        string[][] str;
        q.TransformWords(out str);
        for(int i = 0; i < str.Length;i++){
            string s = "";
            for(int j = 0; j < str[i].Length;j++){
                s += str[i][j] + ",";
            }
            Debug.Log(s);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
