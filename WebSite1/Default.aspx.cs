using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
// using System.Runtime.Serialization.dll; //to json
using System.Web.Script.Serialization;
using System.Dynamic;
using Newtonsoft.Json;


public partial class _Default : Page
{
    string html = null;
    //ExcelReaderListString c = new ExcelReaderListString();

    //List<List<string>> data_patient = null;
    //List<List<string>> data_arrangement = null;
    //List<List<string>> data_arrangement_format = null;

    //List<List<int>> arrangement = null;
    local_data Local_Data = new local_data();
    protected void Page_Load(object sender, EventArgs e)
    {
        string str = System.Environment.CurrentDirectory;
        Local_Data.load_data(Server);
        
        all_table_html(Local_Data.data_arrangement_format);

        tete.Text = Local_Data.get_json();
    }
    
    public string all_table_html(List<List<string>> data) //List<List<int>>
    {
        //根据 表格 arrangement 输出html
        html = null;
        //html = html + " <div>测试测试 ：  后台创建html代码</div>";
        for (int i = 0; i < data.Count; i++)
        {
            if (i == 0)
            { //表头
                html += "<table id=\"diary\" border= 1 width=500px bordercolor=#FBBF00 >" +
                       "<tr><td ></td><td ><center>Lundi</td><td><center>Mardi  </td><td>   Mecredi </td><td>   Jeudi  </td><td>   Vendredi  </td></tr>";
            }
            html += "<tr>"+ "<td > salle " + (i+1)+"</td>";
            for (int j = 0; j < data[i].Count; j++)
            {
                if(data[i][j]!=null&& data[i][j]!="ouvert"&& data[i][j] != "")
                {
                    html += "<td bgcolor =\"#CC4338\">";
                    html += "<select style=\"width: 100px; \" onchange=\"alert(this.value)\" > ";
                    string[] sArray = Regex.Split(data[i][j], ",", RegexOptions.IgnoreCase);
                    foreach (string c in sArray)
                        html += "<option value=\" "+c+"\">" + c+"</option>";
                    html += " </select></td>";
                }
                if (data[i][j] == "ouvert")
                {
                    html += "<td bgcolor=\"#A1F081\" >" + data[i][j] + "</td>";
                }
                if (data[i][j] == "")
                    html += "<td bgcolor=\"#978e9d\"></td>";
            }
            html += "</tr>";
            if (i + 1 == data.Count)
            {
                html += "</table>";
                continue;
            }
        }
        return html;
    }

    public string gethtml()
    {
        return html;
    }
    protected void yyy_Click(object sender, EventArgs e)
    {
        all_table_html(Local_Data.data_arrangement_format); //data_arrangement_format
    }
}