using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SingleGame : MonoBehaviour
{
    // Start is called before the first frame update

    private OutputText output;

    public Text count;
    public Text result;

    public int postNumber;
    private int postedNumber;
    private bool[] postedQuestion = new bool[QuestionCollection.questions.GetLength(0)];

    private bool countF = false;

    private bool finishF = false;
    private float startTime;
    private float endTime;
    void Start()
    {
        output = gameObject.GetComponent<OutputText>(); 
        output.playingF = false;
        //カウントダウン
        StartCoroutine("CountDown");
    }

    // Update is called once per frame
    void Update()
    {
        if(countF){
            if(finishF){
                //リザルト表示
                output.text.text = "";
                output.kana.text = "";
                output.output.text = "";
                endTime = Time.time;
                float WPM = output.allTyping / (endTime - startTime) * 60f;
                float accuracy = (float)(output.allTyping - output.failedTyping) / output.allTyping;

                float score = WPM * accuracy * accuracy * accuracy;
                result.text = score.ToString() + "\n" + WPM.ToString() + "\n" + accuracy.ToString();
            }else if(!output.playingF){
                int number;
                bool flag = true;
                for(int i = 0; i < QuestionCollection.questions.GetLength(0); i++){
                    if(!postedQuestion[i]){
                        flag = false;
                        break;
                    }
                }
                //終了判定
                if(flag || postedNumber >= postNumber){
                    Debug.Log("finish");
                    finishF = true;
                }else{
                    while(true){
                        number = UnityEngine.Random.Range(0, QuestionCollection.questions.GetLength(0));
                        if(!(postedQuestion[number])) break;
                    }

                    output.q = new Question(QuestionCollection.questions[number,0], QuestionCollection.questions[number,1]);
                    postedQuestion[number] = true;
                    output.QuestionInit();        
                    postedNumber++;
                }

            } 
        }
    }

    private IEnumerator CountDown() //コルーチン関数の名前
    {
        count.text = "3";
        yield return new WaitForSeconds(1.0f);
        count.text = "2";
        yield return new WaitForSeconds(1.0f);
        count.text = "1";
        yield return new WaitForSeconds(1.0f);
        count.text = "";
        countF = true;
        startTime = Time.time;
        yield break;
    }


}
