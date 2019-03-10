using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;


public partial class _Default : Page
{
    string html = null;
    Class1 c = new Class1();
    string path_source = "C:/Users/c/source/repos/WebSite1/WebSite1/";

    List<List<string>> data_patient = null;
    List<List<string>> data_arrangement = null;
    List<List<string>> data_arrangement_format = null;

    List<List<int>> arrangement = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        tete.Text = "geygye";
        String xxx = "sadadad";
        // List sbs = new List<List<int>>;
        load_arrangement();
        load_patient();

        former_arrangement();

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
        data_patient = c.rowReadAll("C:/Users/c/source/repos/WebSite1/WebSite1/patients2blocks.xls", 1);
        return data_patient;
    }

    public List<List<string>> load_arrangement()
    {
        data_arrangement = c.rowReadAll("C:/Users/c/source/repos/WebSite1/WebSite1/blocks2or-days.xls", 1); //blocks2or-days.xlsx
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
        if (data_arrangement_format == null)
        {
            data_arrangement_format = new List<List<string>>(data_arrangement.ToArray());//初始化
        }
        for (int salle = 0; salle < data_arrangement.Count; salle++)
        {
            for (int day = 0; day < data_arrangement[salle].Count; day++)
            {
                string all_patient = "";
                if (data_arrangement[salle][day] != "")
                {
                    int arrange = Convert.ToInt32(data_arrangement[salle][day].ToString())-1;
                    for (int patient = 0; patient < data_patient[arrange].Count; patient++)
                    {
                        if (data_patient[arrange][patient] == "1")
                        {
                            all_patient += (patient.ToString() + ",");
                        }
                    }
                    if (all_patient == "")
                    {
                        all_patient += "ouvert";
                    }

                }
                data_arrangement_format[salle][day] = all_patient;
            }
        }
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
        html = null;
        html = html + " <div>测试测试 ：  后台创建html代码</div>";
        for (int i = 0; i < data.Count; i++)
        {
            if (i == 0)
            {
                html += "<table id=\"diary\" border= 1 width=500px bordercolor=#FBBF00 >" +
                       "<tr><td ></td><td ><center>Lundi</td><td><center>Mardi  </td><td>   Mecredi </td><td>   Jeudi  </td><td>   Vendredi  </td></tr>";
            }
            html += "<tr>"+ "<td > salle " + (i+1)+"</td>";
            for (int j = 0; j < data[i].Count; j++)
            {
                //html += "<td>";
                //if (data[i][j] == "" & j==0)
                //{
                //html += data[i][j];
                //}
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
        // data_arrangement
        // data_patient
    }
}