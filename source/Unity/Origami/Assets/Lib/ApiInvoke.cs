//using UnityEngine;
using System;
using System.Collections;


#if WINDOWS_UWP
//using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Web.Http;
using System.Net.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;
using Windows.Data.Json;
#endif
//using System.Net;
using System.Collections.Generic;


public class WebApiInvoke  {


    private Action<int,string> _callback;
    private string _baseUrl;

    public WebApiInvoke(Action<int, string> callback)
    {
        this._callback = callback;
        //this._baseUrl = baseUrl;
    }
#if WINDOWS_UWP
    public async void PostAsyn(string url,Dictionary<string,object> param)
    {
       await PostAsyInner(url, param);


    }

#else
    public void PostAsyn(string url, Dictionary<string, object> param)
    {
        throw new NotImplementedException();
    }
#endif

#if WINDOWS_UWP
    public async void GetAsyn(string url, Dictionary<string, object> param)
    {

       await  GetAsyInner(url, param);

    }
#else
    public void GetAsyn(string url, Dictionary<string, object> param)
    {
        throw new NotImplementedException();
    }
#endif

#if WINDOWS_UWP    

    private async Task  PostAsyInner(string url, Dictionary<string, object> param)
    {
        Console.log("开始请求数据,url:" + url);
        var handle = new HttpBaseProtocolFilter();
        handle.AllowAutoRedirect = false;
        var client = new Windows.Web.Http.HttpClient(handle);// new RestClient();
        var uri = new Uri(url);
        handle.MaxVersion = HttpVersion.Http11;
        var request = new Windows.Web.Http.HttpRequestMessage(Windows.Web.Http.HttpMethod.Post, new Uri(url));
        var jsonstr = new JsonConvert().GetJsonFromObj<Dictionary<string, object>>(param);
        request.Content = new HttpStringContent(jsonstr);
        request.Content.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");
        var resp = await client.SendRequestAsync(request, Windows.Web.Http.HttpCompletionOption.ResponseContentRead);
        await Callback(resp);
        
    }

    private async Task GetAsyInner(string url, Dictionary<string, object> param)
    {
        var handle = new HttpBaseProtocolFilter();
        handle.AllowAutoRedirect = false;
        var client = new Windows.Web.Http.HttpClient(handle);// new RestClient();
        var uri = new Uri(url);
        handle.MaxVersion = HttpVersion.Http11;
        var request = new Windows.Web.Http.HttpRequestMessage(Windows.Web.Http.HttpMethod.Get, new Uri(url));
        var jsonstr = new JsonConvert().GetJsonFromObj<Dictionary<string, object>>(param);
        request.Content = new HttpStringContent(jsonstr);
        request.Content.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");
        var resp = await client.SendRequestAsync(request, Windows.Web.Http.HttpCompletionOption.ResponseContentRead);
        await Callback(resp);
    }

    private async Task Callback(Windows.Web.Http.HttpResponseMessage resp)
    {
        var str = await resp.Content.ReadAsStringAsync();
        var status = (int)resp.StatusCode;
        _callback(status, str);      
        
    }
#endif

}
