using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;

/// <summary>
/// ListlistToJson 的摘要说明
/// </summary>
public class JsonHelper
{
    public string  ListlistToJson(List<List<string>> list_origine)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(list_origine);
        return json;
    }



}