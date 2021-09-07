using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;




public class HttpClient :MonoBehaviour{
    private readonly string baseURL;
    private readonly System.Net.Http.HttpClient httpClient;

    private HttpClient(){    }

    public HttpClient(string baseURL){
        this.baseURL = baseURL;
        this.httpClient = new System.Net.Http.HttpClient();
        this.httpClient.Timeout = System.TimeSpan.FromMilliseconds(10000);
    }

    public string Post(string httpparams){
        Task<System.Net.Http.HttpResponseMessage> response;
        try{
            Debug.Log(this.baseURL + httpparams); 
            response = this.httpClient.SendAsync(new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, this.baseURL + httpparams));

        }catch(System.Net.WebException e){
            return null;
        }
        if(response.Result.StatusCode.Equals(System.Net.HttpStatusCode.OK)){
            
            return System.Text.Encoding.UTF8.GetString(response.Result.Content.ReadAsByteArrayAsync().Result);
        }else{
            return null;
        }
        
    }
}
