using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
// using System.Runtime.Serialization.dll; //to json
//using System.Web.Script.Serialization;
//using System.Dynamic;
//using Newtonsoft.Json;


public partial class _Default : Page
{
    string html = null;
    //ExcelReaderListString c = new ExcelReaderListString();

    //List<List<string>> data_patient = null;
    //List<List<string>> data_arrangement = null;
    //List<List<string>> data_arrangement_format = null;

    //List<List<int>> arrangement = null;
    public local_data Local_Data = new local_data();
    public string data_json;
    bdd_functions bdd = new bdd_functions();

    protected void Page_Load(object sender, EventArgs e)
    {

        string patientsDoc = Request.Form["patientsDoc"];
        if (patientsDoc == null)
        {
            patientsDoc = "test/patients2ors";   //patients2ors   patients2blocks
        }
        string str = System.Environment.CurrentDirectory;
        Local_Data.load_data(Server, patientsDoc);

        all_table_html(Local_Data.Data_arrangement_format);
        data_json = Local_Data.get_json();

        //tete.Text = data_json;
    }

    public string all_table_html(List<List<string>> data) //List<List<int>>
    {
        //根据 表格 arrangement 输出html
        html = null;
        //html = html + " <div>测试测试 ：  后台创建html代码</div>";
        for (int i = 0; i < data.Count; i++)
        {   //鼠标悬浮窗js
            //var tip = document.getElementById("tooltipBlock");
            //var selects = document.getElementById("selects");
            //function tooltip(obj) {
            //    var x = selects.offsetLeft, y = obj.offsetTop, h = obj.offsetHeight, w = selects.offsetWidth;  /*tip.style.width = w + "px";  （这部分是定义显示提示的块的宽度） */
            //    tip.style.marginLeft = x + w + "px"; tip.style.marginTop = y - 30 + "px"; tip.style.display = "block"; tip.innerHTML = "这部分为程序传递（把数据库里相关国家资料传过来）"; }
            //function nodisplay() { tip.style.display = "none"; }

            //html += "<script> <script language=\"javascript\"> function OpenSelectInfo() {var width = 1000;  var height = 500;   var url = \"patient.aspx?id=3\"; window.showModalDialog(url, null, 'dialogWidth=' + width + 'px;dialogHeight=' + height + 'px;help:no;status:no'); }</script>";
            if (i == 0)
            { //表头
                html += "<input type=\"button\" id=\"btn_ModifyNickName\" runat=\"server\" value=\"打开模态窗口\"  style=\"width: 126px;\" onclick=\"OpenSelectInfo()\" />   ";
                html += "<table id=\"diary\" border= 1 width=500px bordercolor=#FBBF00 >" +
                       "<tr><td ></td><td ><center>Lundi</td><td><center>Mardi  </td><td>   Mecredi </td><td>   Jeudi  </td><td>   Vendredi  </td></tr>";
            }
            html += "<tr>" + "<td > salle " + (i + 1) + "</td>";
            for (int j = 0; j < data[i].Count; j++)
            {
                if (data[i][j] != null && data[i][j] != "ouvert" && data[i][j] != "")
                {
                    html += "<td bgcolor =\"#CC4338\"><font color=\"black\">" +
                           Local_Data.getSpecialite()[i][j]
                         + "</font><br>";

                    //html += "<select style=\"width: 130px; \" onchange=\"window.location = this.value;\" > ";
                    html += "<select id=\"testSelect\" style=\"width: 130px; \" onchange=\"return ShowBlock(this.value);\" > ";
                    //this.Response.Write("<script language=javascript>window.open('rows.aspx','newwindow','width=200,height=200')</script>");
                    string[] sArray = Regex.Split(data[i][j], ",", RegexOptions.IgnoreCase);
                    int n = sArray.Count() - 1;
                    html += "<option value= n>" + n + " patients</option>";
                    foreach (string c in sArray)
                    {
                        if (c != "")
                        {
                            DataTable patient;
                            PatientInfos pinfo = new PatientInfos();
                            //patientTest = bdd.select_patient(2);  
                            patient = pinfo.Patient_Load(Server, c);

                            html += "<option  " +
                                "value =\" " + patient.Rows[0][1] + ","+patient.Rows[0][2] +","+patient.Rows[0][3]+","+ patient.Rows[0][5]+","+ patient.Rows[0][6]+","+ patient.Rows[0][7]+ "," + patient.Rows[0][16] + "\">"
                                + patient.Rows[0][1] + " " + patient.Rows[0][2] + "</option>";
                        }
                    }
                    html += " </select></td>";
                }
                else if (data[i][j] == "")
                    html += "<td bgcolor=\"#978e9d\"></td>";
                else
                {
                    html += "<td bgcolor=\"#A1F081\" > <font color=\"black\">" +
                        Local_Data.getSpecialite()[i][j]
                        + "</font><br>Aucun patient </td>";
                }
            }
            html += "</tr>";
            if (i + 1 == data.Count)
            {
                html += "</table>";
                continue;
            }
        }
        html += "<div id=\"MyFormLayer\" style=\"DISPLAY: none; Z - INDEX: 103; LEFT: 16px; WIDTH: 408px; POSITION: absolute; TOP: 24px; HEIGHT: 304px\"> < iframe scrolling = \"no\" frameborder = \"0\" width = \"100%\" height = \"100%\" id = \"IFRAME1\" runat = \"server\" > </ iframe > </ div >";
        return html;
    }

    public string gethtml()
    {
        return html;
    }
    protected void yyy_Click(object sender, EventArgs e)
    {
        all_table_html(Local_Data.Data_arrangement_format); //data_arrangement_format
    }
    
}