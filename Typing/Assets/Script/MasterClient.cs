using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MasterClient : MonoBehaviourPunCallbacks
{

    private Text Question;
    private Text kana;

    public int postNumber;

    private int postedNumber = 0;

    private int[] postedQuestions;

    void Awake()
    {
        Question = GameObject.Find("Question").GetComponent<Text>();
        kana = GameObject.Find("kana").GetComponent<Text>();
    }


    public void GameInit(){
        //問題の設定
        int[] orderArray = Enumerable.Range(0,QuestionCollection.questions.GetLength(0)).ToArray();
        postedQuestions = orderArray.OrderBy(i => System.Guid.NewGuid()).ToArray();
        Array.Resize(ref postedQuestions, postNumber);

    }

    public int[] getQuestionArray(){
        return postedQuestions;
    }

}
