using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

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
        //TODO 重复创建了数据库request 需要优化  比如前一个页面返回来过这些数据，直接调用？？？ 
        bdd_functions bdd = new bdd_functions();
        DataTable patient;
        patient = bdd.select_patient(patientID);
        number.Text = patient.Rows[0][1].ToString();
        name.Text = patient.Rows[0][2].ToString();
        departement.Text = patient.Rows[0][3].ToString();
    }
}