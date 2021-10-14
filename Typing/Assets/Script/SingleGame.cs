using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SingleGame : MonoBehaviour
{
    // Start is called before the first frame update

    private OutputText output;

    public Text count;
    public Text result;

    public Button TitleButton;
    public Button RegisterButton;
    public Slider processBar;

    public int postNumber;

    private int postedNumber = 0;

    private int[] postedQuestions;
    private bool playingF = false;

    private bool countF = false;

    private bool finishF = false;
    private float startTime;
    private float endTime;

    //sound

    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioClip downClip;
    [SerializeField]
    private AudioClip lastClip;

    void Start()
    {
        int[] orderArray = Enumerable.Range(0,QuestionCollection.questions.GetLength(0)).ToArray();

        output = gameObject.GetComponent<OutputText>(); 
        output.typingF = false;
        //問題の設定 シャフル
        postedQuestions = orderArray.OrderBy(i => System.Guid.NewGuid()).ToArray();
        Array.Resize(ref postedQuestions, postNumber);

        //text
        count.text = "<size=40>スペースキーを押してスタート</size>";

        //Button
        TitleButton.gameObject.SetActive(false);
        TitleButton.onClick.AddListener(ChangeSceneTitle);

        RegisterButton.gameObject.SetActive(false);
        RegisterButton.onClick.AddListener(ChangeSceneRegister);

        //Slider
        processBar.maxValue = postNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if(playingF){
            if(finishF){
                //リザルト表示
                output.text.text = "";
                output.kana.text = "";
                output.output.text = "";
                endTime = Time.time;
                float WPM = output.allTyping / (endTime - startTime) * 60f;
                float accuracy = (float)(output.allTyping - output.failedTyping) / output.allTyping;

                float score = WPM * accuracy * accuracy * accuracy;
                result.text = "スコア:" + Mathf.FloorToInt(score).ToString() + "\nWPM:" + Mathf.FloorToInt(WPM).ToString() + "\n正確率:" + Mathf.FloorToInt(accuracy * 100).ToString() + "%";
                playingF = false;
                //ボタン表示
                TitleButton.gameObject.SetActive(true);
                RegisterButton.gameObject.SetActive(true);
                //playerprefにスコア保存
                PlayerPrefs.SetFloat("Score", score);
            }else if(!output.typingF){
                if(postedNumber >= postNumber){
                    Debug.Log("finish");
                    finishF = true;
                    
                    processBar.value = postedNumber;
                }else{

                    output.q = new Question(QuestionCollection.questions[postedQuestions[postedNumber],0], QuestionCollection.questions[postedQuestions[postedNumber],1]);
                    output.QuestionInit();        
                    processBar.value = postedNumber;
                    postedNumber++;
                }

            } 
        }else if(!playingF && finishF){

        }else if(!playingF && !finishF){
            //開始まち
            if(Input.GetKeyUp(KeyCode.Space) && !countF){
                //カウントダウン
                StartCoroutine("CountDown");
                countF = true;
            }
            
        }
    }

    private IEnumerator CountDown() 
    {
        count.text = "3";
        audio.PlayOneShot(downClip, 1.0f);
        yield return new WaitForSeconds(1.0f);
        count.text = "2";
        audio.PlayOneShot(downClip, 1.0f);
        yield return new WaitForSeconds(1.0f);
        count.text = "1";
        audio.PlayOneShot(downClip, 1.0f);
        yield return new WaitForSeconds(1.0f);
        count.text = "";
        audio.PlayOneShot(lastClip, 1.0f);
        playingF = true;
        countF = false;
        startTime = Time.time;
        yield break;
    }

    public void ChangeSceneTitle(){
        SceneManager.LoadScene("Title");
    }

    public void ChangeSceneRegister(){
        SceneManager.LoadScene("Register");
    }

}
