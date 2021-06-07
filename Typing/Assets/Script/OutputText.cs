using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputText : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Text text;

    public Text kana;
    public Text output;
    public Question q{ get; set;}

    public int allTyping{ get; set;}
    public int failedTyping{ get; set;}
    public bool typingF{ get; set;}

    private int skipF = -1;
    private string [][] typingWords;
    private int [] indexArray;
    private int count = 0;
    private int index = 0;
    private int number = 0;
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {

        if(typingF){
            
            TypeKeyBoard();
        }
    }

    public void QuestionInit(){
        Debug.Log(q.q);
        q.TransformWords(out typingWords);
        indexArray = new int[typingWords.Length];
        text.text = q.q;
        kana.text = q.kana;

        output.text = "";
        for(int i =  0; i < typingWords.Length; i++){
            output.text += typingWords[i][0];
        }
        
        typingF = true;
    }

    private void TypeKeyBoard(){
        bool endF = false;
        if (Input.anyKeyDown) {
            // 入力されたキー名
            string keyStr = Input.inputString;
            //特殊キーではなにもしない
            if(keyStr != ""){
                foreach(char c in keyStr){
                    bool failedflag = true;
                    if(c == ' ') continue;
                    allTyping++;
                    //子音をつづけて入力できる"っ"かどうか
                    if(typingWords[count][typingWords[count].Length - 1] == "xtsu" && typingWords[count].Length != 4){
                        if(skipF == -1){
                            for(int k = indexArray[count]; k < typingWords[count].Length; k++){
                                
                                if(typingWords[count][k][index] == c){
                                    //チェック
                                    bool f = true;
                                    for(int i = 0; i < index;i++){
                                        if(typingWords[count][indexArray[count]][i] != typingWords[count][k][i])
                                            f = false;
                                    }
                                    if(!f) continue;
                                    indexArray[count] = k;
                                    if(typingWords[count][k].Length == 1){
                                        skipF = k;
                                        int temp = 0;
                                        while(true){
                                            indexArray[count + temp] = k;
                                            temp++;
                                            if(typingWords[count + temp][typingWords[count + temp].Length - 1] != "xtsu" ||(typingWords[count + temp][typingWords[count + temp].Length - 1] == "xtsu" && typingWords[count + temp].Length == 4)){
                                                for(int i = 0; i < typingWords[count + temp].Length;i++){
                                                    if(typingWords[count + temp][i][0] == c){
                                                        indexArray[count + temp] = i;
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    index++;
                                    if(index == typingWords[count][k].Length){
                                        index = 0;
                                        count++;
                                    }
                                    failedflag = false;
                                    break;
                                }
                            }
                        }else{
                            if(typingWords[count][skipF][index] == c){
                                index++;
                                if(index == typingWords[count][skipF].Length){
                                    index = 0;
                                    count++;
                                }
                                failedflag = false;
                            }
                        }
                    }else{
                        for(int k = indexArray[count]; k < typingWords[count].Length; k++){
                            
                            if(typingWords[count][k][index] == c){

                                if(skipF != -1){
                                    skipF = -1;
                                }else{
                                    //チェック
                                    bool f = true;
                                    for(int i = 0; i < index;i++){
                                        if(typingWords[count][indexArray[count]][i] != typingWords[count][k][i])
                                            f = !f;
                                    }
                                    if(!f) continue;
                                }
                                indexArray[count] = k;
                                index++;
                                if(index == typingWords[count][k].Length){
                                    index = 0;
                                    count++;
                                }
                                failedflag = false;
                                break;
                            }else if(skipF != -1){
                                if('t' == c && typingWords[count - 1][0] == "l"){
                                    //ltuのuの入力まちに変更
                                    count--;
                                    index = 2;
                                    indexArray[count] = indexArray[count] == 0 ? 2 : 3;
                                    Debug.Log(typingWords[count][indexArray[count]]);
                                    skipF = -1;
                                    failedflag = false;
                                    break;
                                }
                            }
                            //省略できる"ん"の二度うちの処理
                            else if(count != 0 && c == 'n' && typingWords[count - 1][typingWords[count - 1].Length - 1] == "xn" && typingWords[count - 1].Length == 3){
                                indexArray[count - 1] = 1;
                                failedflag = false;
                                break;
                            }
                        }
                        
                    }

                    if(failedflag){
                        failedTyping++;
                    }

                }
                //表示
                output.text = "<color=#ff0000>";
                for(int i =  0; i < typingWords.Length; i++){
                    for(int j = 0; j < typingWords[i][indexArray[i]].Length; j++){
                        if(i == count && j == index){
                            endF = true;
                            output.text += "</color>";
                        }
                        output.text += typingWords[i][indexArray[i]][j];
                    }
                    
                }
                if(!endF){
                    output.text += "</color>";
                    count = 0;
                    index = 0;
                    typingF = false;
                    number++;
                }
            }
        }
    }

}
