using System;
using System.Collections.Generic;
using System.Web;
using System.Dynamic;
using Newtonsoft.Json;
using System.Data;

/// <summary>
/// local_data 的摘要说明
/// 未来将所有读取本地 excle 的代码放在这个类中
/// </summary>
public class local_data
{
    ExcelReaderListString c = new ExcelReaderListString();

    List<List<string>> data_patient = null;
    List<List<string>> data_arrangement = null;
    List<List<string>> data_specialite = null;
    bdd_functions bdd = new bdd_functions();
    dynamic data_dic = new ExpandoObject();

    public List<List<string>> data_arrangement_format = null;
    
    public string data_json;

    public List<List<string>> getSpecialite()
    {//TODO 返回的 data_specialite 可能是一个引用 
        return data_specialite;
    }

    public void load_data(HttpServerUtility Server)
    {
        //调用读取 excel 文件存到listlist string 中
        //生成一个json文件
        load_arrangement(Server);
        load_patient(Server);
        update_departement();
        data_json = load_json();
        former_arrangement();
    }

    //TODO 下拉框根据不同的文件来进行导入 
    void load_patient(HttpServerUtility Server)
    {
        // 读取.xls 文件将数据存在 data_patient 中； 尚未支持其他格式的表格文件
        //C:/Users/c/source/repos/WebSite1/WebSite1/       patients2ors.xls   patients2blocks.xls
        string path = Server.MapPath("./App_Data/patients2ors.xls");
        data_patient = c.rowReadAll(path, 1);
     }


    //这一份是固定的，无需改动  
    void load_arrangement(HttpServerUtility Server)
    {
        String path = Server.MapPath("./App_Data/blocks2or-days.xls");
        data_arrangement = c.rowReadAll(path, 1);
        path = Server.MapPath("./App_Data/Spécialités chirurgicales.xls");
        data_specialite = c.rowReadAll(path, 1);
    }

    void update_departement()
    { //bdd 中更新病人的科室
        for (int salle = 0; salle < data_arrangement.Count; salle++)
        {
            for (int day = 0; day < data_arrangement[salle].Count; day++)
            {
                if (data_arrangement[salle][day] != "")
                {
                    int arrangeNumber = Convert.ToInt32(data_arrangement[salle][day].ToString());
                    for (int patient = 0; patient < data_patient[arrangeNumber-1].Count; patient++)
                    {
                        if (data_patient[arrangeNumber-1][patient] == "1")  //如果某一个病人 patient 要在这个 timeblock 动手术
                        {
                            bdd.update_patient_spe(patient + 1, data_specialite[salle][day]);
                        }
                    }
                }
            }
        }
    }



    string load_json()
    {
        bdd_functions bdd = new bdd_functions();
        //创建一个空的 手术室列表   每个手术室门口都贴着一张时刻表 就是 arrangements
        List<ExpandoObject> salles = new List<ExpandoObject>();
        for (int salle = 0; salle < data_arrangement.Count; salle++)
        {
            //新建一个空的手术室， sall.lundi  sall.....   
            dynamic sall = new ExpandoObject();
            sall.Number = salle;
            for (int d = 0; d < 5; d++)
            {   //新建空白的一天
                dynamic day = new ExpandoObject();
                if (data_arrangement[salle][d] == "")
                {
                    day.status = "ferme";
                    day.patient_number = 0;
                }
                else
                {
                    day.status = "ouvert";
                    int block_time = Convert.ToInt32(data_arrangement[salle][d].ToString());
                    day.time_id = block_time;
                    List<ExpandoObject> patients = new List<ExpandoObject>();
                    int NumberPatient = 0;
                    for (int p = 0; p < data_patient[day.time_id - 1].Count; p++)
                    {
                        dynamic patient = new ExpandoObject();
                        if (data_patient[day.time_id - 1][p] == "1")
                        {
                            DataTable patientDataTable;
                            patientDataTable = bdd.select_patient(p);
                            //  patient.id = p;
                            // DataTable to ExpandoObject 
                            patients.Add(patient);
                            NumberPatient += 1;
                        }
                    }
                    day.patient_number = NumberPatient;
                    if (NumberPatient > 0)
                    {
                        day.status = "occupe";
                        day.patients = patients;
                    }
                }
                switch (d)
                {
                    case 0:
                        sall.Lundi = day;
                        break;
                    case 1:
                        sall.Mardi = day;
                        break;
                    case 2:
                        sall.Mercredi = day;
                        break;
                    case 3:
                        sall.Jeudi = day;
                        break;
                    case 4:
                        sall.Vendredi = day;
                        break;
                }
            }
            salles.Add(sall);
        }

        data_dic.salles = salles;

        var dictionary = (IDictionary<string, object>)data_dic;

        var serialized = JsonConvert.SerializeObject(dictionary); // {"Int":3,"String":"hi","Bool":false}
        return serialized;
    }

    public string get_json()
    {
        return this.data_json;
    }

    public void former_arrangement()
    {
        //重组表格的 blocks2or-days 的内容 开放但是没有病人的时候用 ouvert   不开放的为 ""  有病人的为一串字符串
        if (data_arrangement_format == null)
        {
            data_arrangement_format = new List<List<string>>(data_arrangement.ToArray());//初始化 TODO  如果修改这个同时会修改掉list    data_arrangement  需要改成深拷贝
        }
        for (int salle = 0; salle < data_arrangement.Count; salle++)
        {
            for (int day = 0; day < data_arrangement[salle].Count; day++)
            {
                string all_patient = ""; // 初始化所有为空
                int p = 0;
                if (data_arrangement[salle][day] != "")
                {
                    int arrange = Convert.ToInt32(data_arrangement[salle][day].ToString()) - 1;
                    for (int patient = 0; patient < data_patient[arrange].Count; patient++)
                    {
                        if (data_patient[arrange][patient] == "1")  //如果某一个病人 patient 要在这个 timeblock 动手术
                        {
                            //patients[p] = patient;
                            //p += 1;
                            // 循环中 patient 的id 从0 开始，    数据库中的id从1开始
                            all_patient += ((patient+1).ToString() + ",");
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

}