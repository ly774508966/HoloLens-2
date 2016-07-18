using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

using System.Reflection;
using System.Xml;
using System.Globalization;
#if WINDOWS_UWP
using RestSharp.Extensions;
using Windows.Data.Json;
#endif
using System.Collections;

public class JsonConvert
{

    public string GetJsonFromObj<T>(T obj)
    {
#if WINDOWS_UWP
        return new RestSharp.Serializers.JsonSerializer().Serialize(obj);       
#else
        throw new NotImplementedException();
#endif
    }

    public T GetItemFromJson<T>(string json)
    {
#if WINDOWS_UWP
        return new RestSharp.Deserializers.JsonDeserializer().Deserialize<T>(json);
#else
        throw new NotImplementedException();
#endif
    }
}



   

 