using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;




public class HttpClient :MonoBehaviour{
    private readonly string baseURL;
    private UnityWebRequest httpClient;
    private WWWForm form;

    public IEnumerator httpRequestPost (string baseURL, string httpparams){
        httpClient = UnityWebRequest.Post(baseURL+httpparams, new WWWForm());
        yield return httpClient.SendWebRequest();
        if (httpClient.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(httpClient.error);
        }
        else
        {
            Debug.Log(httpClient.downloadHandler.text);
        }
    }

}
