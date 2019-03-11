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
    ExcelReaderListString c = new ExcelReaderListString();
    //string path_source = "C:/Users/c/source/repos/WebSite1/WebSite1/";

    List<List<string>> data_patient = null;
    List<List<string>> data_arrangement = null;
    List<List<string>> data_arrangement_format = null;

    List<List<int>> arrangement = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        string str = System.Environment.CurrentDirectory;

        //String xxx = "sadadad";
        // List sbs = new List<List<int>>;
        load_arrangement();
        load_patient();
        tete.Text = Test();

        former_arrangement();

        //JsonHelper j = new JsonHelper();
        //tete.Text = j.ListlistToJson(data_arrangement_format);

        all_table_html(data_arrangement_format);
       


        //    DataTable dt = new DataTable();
        //    DataSet ds = new DataSet();
        //    DataTable dt1 = new DataTable();
        //    //dt1=ds.Tables
        //    bdd_functions bdd = new bdd_functions();
        //    DataTable dt2 = bdd.select_patient(90); 

        //    tete.Text = dt2.Rows[0]["name"].ToString();
        //    bdd.insert_patient(500);
    }

    public List<List<string>> load_patient()
    {
        // 读取.xls 文件将数据存在 data_patient 中； 尚未支持其他格式的表格文件
        string path = Server.MapPath("./patients2blocks.xls");
      
        data_patient = c.rowReadAll(path, 1);//C:/Users/c/source/repos/WebSite1/WebSite1/
        return data_patient;
    }

    public List<List<string>> load_arrangement()
    {
        String path2 = Server.MapPath("./blocks2or-days.xls");
        data_arrangement = c.rowReadAll(path2, 1); //C:/Users/c/source/repos/WebSite1/WebSite1/blocks2or-days.xls   ..//
        return data_arrangement;
    }

    //public void former_arrangement(List<List<string>> data)
    //{
    
    //    for(int i=0;i< data_arrangement.Count;i++)
    //    {
    //        for (int j = 0; j <5; j++)
    //        {
    //            if (arrangement == null)
    //            {
    //                arrangement = new List<List<int>>();//初始化
    //            }
    //            if (data[i][j] != "")
    //            {
    //                List<int> l = new List<int> { Convert.ToInt32(data[i][j].ToString()), i, j+1};
    //                arrangement.Add(l);
    //            }
    //        }
    //    }
    //}


    public void former_arrangement()
    {
        //重组表格的 blocks2or-days 的内容 开放但是没有病人的时候用 ouvert   不开放的为 ""
        if (data_arrangement_format == null)
        {
            data_arrangement_format = new List<List<string>>(data_arrangement.ToArray());//初始化  如果修改这个可能同时会修改掉list    data_arrangement
        }
        for (int salle = 0; salle < data_arrangement.Count; salle++)
        {
            for (int day = 0; day < data_arrangement[salle].Count; day++)
            {
                string all_patient = ""; // 初始化所有为空
                int p = 0;
                if (data_arrangement[salle][day] != "")
                {
                    int arrange = Convert.ToInt32(data_arrangement[salle][day].ToString())-1;
                    for (int patient = 0; patient < data_patient[arrange].Count; patient++)
                    {
                        if (data_patient[arrange][patient] == "1")
                        {
                            //patients[p] = patient;
                            //p += 1;
                            all_patient += (patient.ToString() + ",");
                        }
                    }
                    if (all_patient == "")
                    {
                        all_patient += "ouvert";
                    }

                }
                data_arrangement_format[salle][day] = all_patient; //patients
            }
        }
    }

    public string Test()
    {
        dynamic flexible = new ExpandoObject();

        //创建一个空的 手术室列表   每个手术室门口都贴着一张时刻表 就是 arrangements
        List<ExpandoObject> salles = new List<ExpandoObject>();

        for (int salle = 0; salle < data_arrangement.Count; salle++)
        {
            //新建一个空的手术室， sall.lundi  sall.....
            dynamic sall = new ExpandoObject();
            for (int d = 0; d < 5; d++)
            {   //新建空白的一天
                dynamic day = new ExpandoObject();
                if (data_arrangement[salle][d] == "")
                {
                    day.status = "ferme";
                }
                else
                {
                    day.status = "open";
                    int block_time = Convert.ToInt32(data_arrangement[salle][d].ToString());
                    day.time_id = block_time;
                    List<ExpandoObject> patients = new List<ExpandoObject>();
                    int NumberPatient = 0;
                    for (int p = 0; p < data_patient[day.time_id - 1].Count; p++)
                    {
                        dynamic patient = new ExpandoObject();
                        if (data_patient[day.time_id - 1][p] == "1")
                        {
                            patient.id = p;
                            patients.Add(patient);
                            NumberPatient += 1;
                        }
                    }
                    day.patient_number = NumberPatient;
                    if (NumberPatient > 0)
                    {
                        day.patients = patients;
                    }
                }
                switch (d)
                {
                    case 0:
                        sall.lundi = day;
                        break;
                    case 1:
                        sall.mardi = day;
                        break;
                    case 2:
                        sall.mercredi = day;
                        break;
                    case 3:
                        sall.jeudi = day;
                        break;
                    case 4:
                        sall.vendredi = day;
                        break;
                }
                //if (d==0)
                //    sall.lundi = day;
            }
            salles.Add(sall);
        }

        flexible.salles = salles;

        //dynamic flexible2 = new ExpandoObject();
        //flexible2.a = "aaaaa";
        //flexible2.b = "bbbbb";


        //dynamic flexible3 = new ExpandoObject();
        //flexible3.a = "aaaaa";
        //flexible3.b = "bbbbb";
        //flexible3.c = "ccc";


        //List<ExpandoObject> l = new List<ExpandoObject> { flexible2, flexible2, flexible2 };

        //flexible.test = flexible2;
        //flexible.Int = 3;
        //l.Add(flexible3);
        //flexible.l = l;
        //flexible.String = "hi";

        var dictionary = (IDictionary<string, object>)flexible;

        var serialized = JsonConvert.SerializeObject(dictionary); // {"Int":3,"String":"hi","Bool":false}
        return serialized;
    }

    public DataTable todatable( List<List<string>> d)
    {
        DataTable dt = new DataTable();
        foreach(List<string> row in d)
        {
            dt.Rows.Add(row.ToArray());
        }
        return dt;
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
    //public string all_table_html(List<List<string>> data)
    //{
    //    html = null;
    //    html = html + "<div>测试测试 ：  后台创建html代码</div>";
    //    for (int i = 0; i < data.Count; i++)
    //    {
    //        //html = html + "<div>";
    //        if (data[i][1] == "" & data[i][0] != "")
    //        {
    //            int semaine_number = Convert.ToInt32(data[i][0].ToString()) + 1;
    //            html += "<table border=\"1\" width=500px bordercolor=#FBBF00>" +
    //                "<tr><td> semaine" + semaine_number + "<td></tr>";
    //            continue;
    //        }
    //        else if (data[i][1] == "" & data[i][0] == "")
    //        {
    //            html += "<table>";
    //        }
    //        html += "<tr>";
    //        for (int j = 0; j < data[i].Count; j++)
    //        {
    //            html += "<td>";
    //            //if (data[i][j] == "" & j==0)
    //            //{
    //            html += data[i][j];
    //            //}
    //            html += "</td>";
    //        }
    //        html += "</tr>";
    //        if (i + 1 == data.Count)
    //        {
    //            html += "</table>";
    //            continue;
    //        }
    //        else if (data[i + 1][0] == "")
    //        {
    //            html += "</table>";
    //            continue;
    //        }
    //        //html += "</div>";
    //    }
    //    return html;
    //} 

    public string gethtml()
    {
        return html;
    }
    protected void yyy_Click(object sender, EventArgs e)
    {
        all_table_html(data_arrangement_format); //data_arrangement_format
    }
}