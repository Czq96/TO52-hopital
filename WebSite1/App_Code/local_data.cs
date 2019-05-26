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

    List<List<string>> data_patient_information = null; 
    List<List<string>> data_patient_ors = null;
    List<List<string>> data_patient_icu = null;
    List<List<string>> data_arrangement = null;
    List<List<string>> data_arrange_specialite = null;
    private List<List<string>> data_arrangement_format = null;
    private string data_json;

    bdd_functions bdd = new bdd_functions();
    dynamic data_dic = new ExpandoObject();

    public void load_data(HttpServerUtility Server, string patientsDoc)
    {
        //调用读取 excel 文件存到listlist string 中
        //生成一个json文件
        loadPatientInformation(Server, "test/patients_information");  //引号中不能有空格 patients2icu.xls
        loadArrangement(Server, "blocks2or-days");
        loadPatientOrs(Server, patientsDoc);  //引号中不能有空格 patients2ors.xls
        loadPatientIcu(Server, "test/patients2icu");  //引号中不能有空格 patients2icu.xls
        updatePatientInformation();
        updatePatientDepartement();
        data_json = load_json();
        //Data_json ="{}";
        former_arrangement();
    }

    void loadPatientInformation(HttpServerUtility Server, string FileName)
    {
        // 读取.xls 文件将数据存在 data_patient_ors 中； 尚未支持其他格式的表格文件
        //C:/Users/c/source/repos/WebSite1/WebSite1/       patients2ors.xls   patients2blocks.xls
        string path = Server.MapPath("./App_Data/" + FileName + ".csv");//xls
        Data_patient_information = c.rowReadAll(path, 1);
    }

    //TODO 下拉框根据不同的文件来进行导入 
    void loadPatientOrs(HttpServerUtility Server, string FileName)
    {
        // 读取.xls 文件将数据存在 data_patient_ors 中； 尚未支持其他格式的表格文件
        //C:/Users/c/source/repos/WebSite1/WebSite1/       patients2ors.xls   patients2blocks.xls
        string path = Server.MapPath("./App_Data/"+FileName+".csv");//xls
        Data_patient_ors = c.rowReadAll(path, 1);
     }

    void loadPatientIcu(HttpServerUtility Server, string FileName)
    {
        // 读取.xls 文件将数据存在 data_patient_ors 中； 尚未支持其他格式的表格文件
        //C:/Users/c/source/repos/WebSite1/WebSite1/       patients2ors.xls   patients2blocks.xls
        string path = Server.MapPath("./App_Data/" + FileName + ".csv");//xls
        Data_patient_icu = c.rowReadAll(path, 1);
    }

    //可以根据file name进行选择
    void loadArrangement(HttpServerUtility Server, string FileName)
    {
        String path = Server.MapPath("./App_Data/"+FileName+ ".csv");
        Data_arrangement = c.rowReadAll(path, 1);
        path = Server.MapPath("./App_Data/Specialites_chirurgicales.csv");
        Data_arrange_specialite = c.rowReadAll(path, 1);
    }

    void updatePatientDepartement()
    { //bdd 中更新病人的各种信息  TODO: 病人中新建 arrangement,根据arrangement来修改参数
        for (int salle = 0; salle < Data_arrangement.Count; salle++)
        {
            for (int day = 0; day < Data_arrangement[salle].Count; day++)
            {
                if (Data_arrangement[salle][day] != "")
                {
                    int arrangeNumber = Convert.ToInt32(Data_arrangement[salle][day].ToString());
                    for (int patient = 0; patient < Data_patient_ors[arrangeNumber-1].Count; patient++)
                    {
                        if (Data_patient_ors[arrangeNumber-1][patient] == "1")  //如果某一个病人 patient 要在这个 timeblock 动手术
                        {
                            bdd.update_patient_departement(patient + 1, Data_arrange_specialite[salle][day]);
                        }
                    }
                }
            }
        }
    }

    void updatePatientInformation()
    {
        List<String> specialies = new List<String>(
                    new string[]{
                        "otolaryngologique",
                        "gynlaryngolog",
                        "orthopyngolo",
                        "neurologique",
                        "geurolog",
                        "ophtalmologique",
                        "vasculaire",
                        "cardiaque",
                        "urologique",
                    }
                );
        if (Data_patient_information!=null)
        {
            for (int patient = 0; patient < Data_patient_information.Count; patient++)
            {
                bdd.update_patient_specialty(patient+1, Convert.ToInt32(Data_patient_information[patient][0]));
                bdd.update_patient_departement(patient + 1,specialies[Convert.ToInt32(Data_patient_information[patient][0])-1]);
                bdd.update_patient_urgencyLevel(patient + 1, Convert.ToInt32(Data_patient_information[patient][1]));
                bdd.update_patient_waitingTime(patient + 1, Convert.ToInt32(Data_patient_information[patient][2]));
                bdd.update_patient_maxWaitingTime(patient + 1, Convert.ToInt32(Data_patient_information[patient][3]));
              
            }
        }
    }


    string load_json()
    {
        bdd_functions bdd = new bdd_functions();
        //创建一个空的 手术室列表   每个手术室门口都贴着一张时刻表 就是 arrangements
        List<ExpandoObject> salles = new List<ExpandoObject>();
        for (int salle = 0; salle < Data_arrangement.Count; salle++)
        {
            //新建一个空的手术室， sall.lundi  sall.....   
            dynamic sall = new ExpandoObject();
            sall.Number = salle;
            for (int d = 0; d < 5; d++)
            {   //新建空白的一天
                dynamic day = new ExpandoObject();
                if (Data_arrangement[salle][d] == "")
                {
                    day.status = "ferme";
                    day.patient_number = 0;
                }
                else
                {
                    day.status = "ouvert";
                    int block_time = Convert.ToInt32(Data_arrangement[salle][d].ToString());
                    day.time_id = block_time;
                    List<ExpandoObject> patients = new List<ExpandoObject>();
                    int NumberPatient = 0;
                    for (int p = 0; p < Data_patient_ors[day.time_id - 1].Count; p++)
                    {
                        dynamic patient = new ExpandoObject();
                        if (Data_patient_ors[day.time_id - 1][p] == "1")
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
        return this.Data_json;
    }

    public void former_arrangement()
    {
        //重组表格的 blocks2or-days 的内容 开放但是没有病人的时候用 ouvert   不开放的为 ""  有病人的为一串字符串
        if (Data_arrangement_format == null)
        {
            Data_arrangement_format = new List<List<string>>(Data_arrangement.ToArray());//初始化 TODO  如果修改这个同时会修改掉list    data_arrangement  需要改成深拷贝
        }
        for (int salle = 0; salle < Data_arrangement.Count; salle++)
        {
            for (int day = 0; day < Data_arrangement[salle].Count; day++)
            {
                string all_patient = ""; // 初始化所有为空
                int p = 0;
                if (Data_arrangement[salle][day] != "")
                {
                    int arrange = Convert.ToInt32(Data_arrangement[salle][day].ToString()) - 1;
                    for (int patient = 0; patient < Data_patient_information.Count; patient++)//Data_patient_information.Count 返回病人的数量
                    {
                        if (Data_patient_ors[arrange][patient] == "1")  //如果某一个病人 patient 要在这个 timeblock 动手术
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
                Data_arrangement_format[salle][day] = all_patient; //patients
            }
        }
    }


    //封装
    public List<List<string>> Data_patient_information
    {
        get
        {
            return data_patient_information;
        }

        set
        {
            data_patient_information = value;
        }
    }

    public List<List<string>> Data_patient_ors
    {
        get
        {
            return data_patient_ors;
        }

        set
        {
            data_patient_ors = value;
        }
    }

    public List<List<string>> Data_patient_icu
    {
        get
        {
            return data_patient_icu;
        }

        set
        {
            data_patient_icu = value;
        }
    }

    public List<List<string>> Data_arrangement
    {
        get
        {
            return data_arrangement;
        }

        set
        {
            data_arrangement = value;
        }
    }

    public List<List<string>> Data_arrange_specialite
    {
        get
        {
            return data_arrange_specialite;
        }

        set
        {
            data_arrange_specialite = value;
        }
    }

    public List<List<string>> Data_arrangement_format
    {
        get
        {
            return data_arrangement_format;
        }

        set
        {
            data_arrangement_format = value;
        }
    }

    public string Data_json
    {
        get
        {
            return data_json;
        }

        set
        {
            data_json = value;
        }
    }

    public List<List<string>> getSpecialite()
    {
        return Data_arrange_specialite;
    }

}