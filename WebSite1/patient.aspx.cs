using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Spire.Xls;
using NPOI;
using NPOI.HSSF;
using Spire.Xls.Charts;
using System.Drawing.Imaging;

public partial class patient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string pID = Request.QueryString["id"];
        int patientID = 1; //设置一个默认值
        if(pID != null)
        {
            patientID = Convert.ToInt32(pID);
        }
        bd.Text = patientID.ToString();
        
        //TODO 重复创建了数据库request 需要优化  
        bdd_functions bdd = new bdd_functions();
        DataTable patient;
        patient = bdd.select_patient(patientID);
        number.Text = patient.Rows[0][1].ToString();
        name.Text = patient.Rows[0][2].ToString();
        departement.Text = patient.Rows[0][3].ToString();
        urgencyLevel.Text = patient.Rows[0][5].ToString();
        waitingTime.Text = patient.Rows[0][6].ToString();
        maxWaitingTime.Text = patient.Rows[0][7].ToString();
        icuImage.ImageUrl = GenerGraphic(patient,Convert.ToInt32(patient.Rows[0][1].ToString()));
        //icuImage.ImageUrl = "~/temps/patient_88.jpeg";
        //TextBox1.Text = icuImage.ImageUrl;
    }

    //返回一个图片的路径
    public String GenerGraphic(DataTable result, int number)
    {
        Workbook patientICU = new Workbook();
        //TODO:使用临时文件
        string filename = "patient_" + number.ToString()+".csv";

        //文件数据
        Worksheet sheet = patientICU.Worksheets[0];
        List<String> weekdays = new List<string> { "weekdays","Monday",
            "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday",};
        for (int i = 0; i< 8; i++) {
            sheet.Range["A"+(i+1).ToString()].Value = weekdays[i];
            if (i != 0)
                sheet.Range["B" + (i + 1).ToString()].NumberValue = Convert.ToInt32(result.Rows[0][i + 8]);
            else
                sheet.Range["B" + (i + 1).ToString()].Value = " ";
        }
       
        //添加图表 簇状图
        Chart chartICU = sheet.Charts.Add(ExcelChartType.BarClustered);
        chartICU.PlotArea.Visible = false;
        chartICU.SeriesDataFromRange = true;

        //指定图表所在的位置
        chartICU.Width = 395;
        chartICU.Height= 410;
        chartICU.LeftColumn = 1;
        chartICU.TopRow = 10;

        //选择数据范围
        chartICU.DataRange = sheet.Range["$A$1:$B$8"];

        chartICU.ChartTitle = "";
        // y
        chartICU.PrimaryCategoryAxis.Title = "Jours de la semaine";

        // x
        chartICU.PrimaryValueAxis.Title = "Utiliser USI ou non";
        chartICU.PrimaryValueAxis.MajorTickMark = TickMarkType.TickMarkOutside;
        chartICU.PrimaryValueAxis.MinorTickMark = TickMarkType.TickMarkInside;
        chartICU.PrimaryValueAxis.TickLabelPosition = TickLabelPositionType.TickLabelPositionNextToAxis;
        chartICU.PrimaryValueAxis.MinValue = 0;
        chartICU.PrimaryValueAxis.MaxValue = 1;
        chartICU.PrimaryValueAxis.MinorUnit = 1;


        chartICU.Legend.Position = LegendPositionType.Right;

        //保存文件
        //patientICU.SaveToFile(filename);
        string path = Server.MapPath("/temps/" + filename);
        patientICU.SaveToFile(path);

        //Image[] images = patientICU.SaveChartAsImage(sheet);
        System.Drawing.Image[] images = patientICU.SaveChartAsImage(sheet);
        string imageName = "patient_" + number.ToString() + ".jpeg";
        string imagePath = Server.MapPath("/temps/" + imageName);
        for (int i = 0; i < images.Length; i++)
        {
            images[i].Save(string.Format(imagePath, i), ImageFormat.Jpeg);
        }
        String imageDynaPath = "~/temps/" + imageName;
        return imageDynaPath;
    }
}