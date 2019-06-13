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
using Spire.Xls;
using Spire.Xls.Charts;
using System.Drawing.Imaging;


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
    string imgSalleGraphicPath;
    string imgDayGraphicPath;
    string imgSpecialityGraphicPath;

    protected void Page_Load(object sender, EventArgs e)
    {

        string patientsDoc = Request.Form["patientsDoc"];
        if (patientsDoc == null)
        {
            patientsDoc = "test/patients2ors";   //patients2ors   patients2blocks
        }
        string str = System.Environment.CurrentDirectory;
        Local_Data.load_data(Server, patientsDoc);

        imgSalleGraphicPath = generateGraphicSallePatient(Local_Data.Data_arrangement_format);
        imgDayGraphicPath = generateGraphicDayPatient(Local_Data.Data_arrangement_format);
        imgSpecialityGraphicPath = generateSpecialitePatientGraphic();

        all_table_html(Local_Data.Data_arrangement_format);
        data_json = Local_Data.get_json();

        //tete.Text = data_json;
    }

    public string all_table_html(List<List<string>> data) //List<List<int>>
    {
        //根据 表格 arrangement 输出html
        html = null;
      //  html = html + " <div>测试测试 ：  后台创建html代码</div>";
        for (int i = 0; i < data.Count; i++)
        {   //鼠标悬浮窗js
            //var tip = document.getElementById("tooltipBlock");
            //var selects = document.getElementById("selects");
            //function tooltip(obj) {
            //    var x = selects.offsetLeft, y = obj.offsetTop, h = obj.offsetHeight, w = selects.offsetWidth;  /*tip.style.width = w + "px";  （这部分是定义显示提示的块的宽度） */
            //    tip.style.marginLeft = x + w + "px"; tip.style.marginTop = y - 30 + "px"; tip.style.display = "block"; tip.innerHTML = "这部分为程序传递（把数据库里相关国家资料传过来）"; }
            //function nodisplay() { tip.style.display = "none"; }

            //html += "<script> <script language=\"javascript\"> function OpenSelectInfo() {var width = 1000;  var height = 500;   var url = \"patient.aspx?id=3\"; window.showModalDialog(url, null, 'dialogWidth=' + width + 'px;dialogHeight=' + height + 'px;help:no;status:no'); }</script
            if (i == 0)
            { //表头
                html += "<table id=\"diary\" border= 1 width=500px bordercolor=#FBBF00 >" +
                       "<tr><td ></td><td ><center>Lundi</td><td><center>Mardi  </td><td>   Mecredi </td><td>   Jeudi  </td><td>   Vendredi  </td></tr>";
            }
            html += "<tr>" + "<td > salle " + (i + 1) + "</td>";
            for (int j = 0; j < data[i].Count; j++)
            {
                if (data[i][j] != null && data[i][j] != "ouvert" && data[i][j] != "")
                {
                    html += "<td style=\"word-break: break-all;white-space: normal;\" bgcolor =\"#CC4338\"><font color=\"black\">" +
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
                                "value =\" " + patient.Rows[0][1] + ","+patient.Rows[0][2] +","+patient.Rows[0][3]+","+ patient.Rows[0][5]+","+ patient.Rows[0][6]+","+ patient.Rows[0][7]+ "," + patient.Rows[0][24] + "\">"
                                + "N°" + patient.Rows[0][1] + " " + patient.Rows[0][2] + "</option>";
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
     
        
    public string getSalleImagePath()
    {
        return imgSalleGraphicPath;
    }

    public string getDayImagePath()
    {
        return imgDayGraphicPath;
    }

    public string getSpecialityImagePath()
    {
        return imgSpecialityGraphicPath;
    }

    protected string generateGraphicSallePatient(List<List<string>> data)
    {
        Workbook patientOrs = new Workbook();
        Worksheet sheet = patientOrs.Worksheets[0];

        sheet.Range["A1"].Value = "salle number";
        sheet.Range["B1"].Value = "patient number";

        //在sheet中添加数据
        for (int salle = 0; salle < data.Count; salle++) {
            sheet.Range["A" + (salle+2).ToString()].Value = "salle" + (salle+1).ToString();
            int personCount = 0;
            for (int day = 0; day < 5; day++)
            {
                if (data[salle][day] != "" && data[salle][day] != null && data[salle][day] != "ouvert") {
                    string[] sArray = Regex.Split(data[salle][day], ",", RegexOptions.IgnoreCase);
                    personCount = sArray.Count() - 1;
                }
            }
            sheet.Range["B" + (salle + 2).ToString()].NumberValue = personCount;
        }

        //创建饼图 
        Chart chartSalle = sheet.Charts.Add(ExcelChartType.Pie);
        
        chartSalle.PlotArea.Visible = false;
        chartSalle.SeriesDataFromRange = true;

        //Set region of chart data
        chartSalle.DataRange = sheet.Range["B2:B11"];
        chartSalle.SeriesDataFromRange = false;

        Spire.Xls.Charts.ChartSerie cs = chartSalle.Series[0];

        //labels
        cs.CategoryLabels = sheet.Range["A2:A11"];

        // Set the value visible in the chart
        cs.DataPoints.DefaultDataPoint.DataLabels.HasLegendKey = true;
        cs.DataPoints.DefaultDataPoint.DataLabels.HasValue = true;
        cs.DataPoints.DefaultDataPoint.DataLabels.HasCategoryName = true;
        cs.DataPoints.DefaultDataPoint.DataLabels.Position = DataLabelPositionType.Outside;
        cs.DataPoints.DefaultDataPoint.DataLabels.Delimiter = "-";

        cs.DataPoints.DefaultDataPoint.DataLabels.FrameFormat.Fill.Texture = GradientTextureType.Papyrus;

        chartSalle.ChartTitle = "Nombre de patient de chaque salle dans une semaine";

        string path = Server.MapPath("/temps/1sallePatient.csv");
        patientOrs.SaveToFile(path);

        System.Drawing.Image[] images = patientOrs.SaveChartAsImage(sheet);
        string imageName = "/temps/salle_use.jpeg";
        string imagePath = Server.MapPath(imageName);
        for (int i = 0; i < images.Length; i++)
        {
            images[i].Save(string.Format(imagePath, i), ImageFormat.Jpeg);
        }
        return imageName;
    }

    protected string generateGraphicDayPatient(List<List<string>> data)
    {
        Workbook patientOrs = new Workbook();
        Worksheet sheet = patientOrs.Worksheets[0];
   
        sheet.Range["A1"].Value = "weekdays";
        sheet.Range["B1"].Value = "patient number";

        List < String > weekdays = new List<string> { "weekdays","Lundi",
            "Mardi", "Mercredi", "Jeudi", "Vendredi",};
        //在sheet中添加数据
        for (int day = 1; day < 6; day++)
        {
            sheet.Range["A" + (day +1).ToString()].Value = weekdays[day];
            int personCount = 0;
            for (int salle = 0; salle < data.Count; salle++)
            {
                if (data[salle][day-1] != "" && data[salle][day - 1] != null && data[salle][day - 1] != "ouvert")
                {
                    string[] sArray = Regex.Split(data[salle][day - 1], ",", RegexOptions.IgnoreCase);
                    personCount = sArray.Count() - 1;
                }
            }
            sheet.Range["B" + (day+1).ToString()].NumberValue = personCount;
        }

        //创建饼图 
        Chart chartSalle = sheet.Charts.Add(ExcelChartType.Pie);

        chartSalle.PlotArea.Visible = false;
        chartSalle.SeriesDataFromRange = true;

        //Set region of chart data
        chartSalle.DataRange = sheet.Range["B2:B6"];
        chartSalle.SeriesDataFromRange = false;

        Spire.Xls.Charts.ChartSerie cs = chartSalle.Series[0];

        //labels
        cs.CategoryLabels = sheet.Range["A2:A6"];

        // Set the value visible in the chart
        cs.DataPoints.DefaultDataPoint.DataLabels.HasValue = true;
        cs.DataPoints.DefaultDataPoint.DataLabels.HasCategoryName = true;
        cs.DataPoints.DefaultDataPoint.DataLabels.Position = DataLabelPositionType.Outside;
        //cs.DataPoints.DefaultDataPoint.DataLabels.Delimiter = "\n";
        cs.DataPoints.DefaultDataPoint.DataLabels.FrameFormat.Fill.Texture = GradientTextureType.Papyrus;
        cs.DataPoints.DefaultDataPoint.DataLabels.HasLegendKey = true;

        chartSalle.ChartTitle = "Nombre de patient de chaque jour dans une semaine";
        
        string path = Server.MapPath("/temps/1dayPatient.csv");
        patientOrs.SaveToFile(path);

        System.Drawing.Image[] images = patientOrs.SaveChartAsImage(sheet);
        string imageName = "/temps/day_use.jpeg";
        string imagePath = Server.MapPath(imageName);
        for (int i = 0; i < images.Length; i++)
        {
            images[i].Save(string.Format(imagePath, i), ImageFormat.Jpeg);
        }
        return imageName;
    }

    protected string generateSpecialitePatientGraphic()
    {
        Workbook patientOrs = new Workbook();
        Worksheet sheet = patientOrs.Worksheets[0];
        List<String> FirstLine = new List<string> { "  ","Avoir Opération",
            "Attendre Opération", "All"};
        for (int i = 0; i < 4; i++)
        {
            sheet.Range["A" + (i + 1).ToString()].Value = FirstLine[i];
        }
        List<String> Colone = new List<string> {
        "A","B","C","D","E","F","G","H","I","J","K","L"
        };
        List<String> FirstColone = new List<string> {
            "otolaryngologique",
            "gynlaryngolog",
            "orthopyngolo",
            "neurologique",
            "geurolog",
            "ophtalmologique",
            "vasculaire",
            "cardiaque",
            "urologique"};

        //在sheet中添加数据
        for (int specialite = 0; specialite < FirstColone.Count; specialite++)
        {
            sheet.Range[Colone[specialite + 1] + "1"].Value = FirstColone[specialite];
           
            int patientNumber = bdd.get_patients_number_by_specialty(specialite + 1);
            int patientNumberHasOperation = bdd.get_patients_number_hasOperation_by_specialty(specialite + 1);

            sheet.Range[Colone[specialite + 1] + "2"].NumberValue = patientNumber;
            sheet.Range[Colone[specialite + 1] + "3"].NumberValue = patientNumberHasOperation;
            sheet.Range[Colone[specialite + 1] + "4"].NumberValue = patientNumber-patientNumberHasOperation;
        }

        //创建柱状图 
        //        Chart chartSalle = sheet.Charts.Add(ExcelChartType.ColumnClustered);
        Chart chartSpeciatity = sheet.Charts.Add(ExcelChartType.ColumnStacked);
        chartSpeciatity.PlotArea.Visible = false;
        chartSpeciatity.SeriesDataFromRange = true;

        //选择数据范围
        chartSpeciatity.DataRange = sheet.Range["A2:J3"];

        chartSpeciatity.LeftColumn = 1;
        chartSpeciatity.TopRow = 15;
        chartSpeciatity.RightColumn = 20;
        chartSpeciatity.BottomRow = 40;

        Spire.Xls.Charts.ChartSerie cs = chartSpeciatity.Series[0];
        chartSpeciatity.Series[1].DataPoints.DefaultDataPoint.DataLabels.HasValue = true;
        //labels
        cs.CategoryLabels = sheet.Range["B1:J1"];

        // Set the value visible in the chart
        cs.DataPoints.DefaultDataPoint.DataLabels.HasValue = true;
        cs.DataPoints.DefaultDataPoint.DataLabels.Delimiter = "-";

        chartSpeciatity.ChartTitle = "Nombre de patient de spécialité dans une semaine";

        chartSpeciatity.PrimaryCategoryAxis.Title = "spécialité";
        chartSpeciatity.PrimaryValueAxis.Title = "numbre des patients";
      
        string path = Server.MapPath("/temps/SpecialiteJourPatient.csv");
        patientOrs.SaveToFile(path);

        System.Drawing.Image[] images = patientOrs.SaveChartAsImage(sheet);
        string imageName = "/temps/speciciate_all.jpeg";
        string imagePath = Server.MapPath(imageName);
        for (int i = 0; i < images.Length; i++)
        {
            images[i].Save(string.Format(imagePath, i), ImageFormat.Jpeg);
        }
        return imageName;
    }


}