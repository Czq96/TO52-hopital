using System;
using System.Collections.Generic;
using System.Web;
using System.Dynamic;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

/// <summary>
/// local_data 的摘要说明
/// 读取excel文档的数据,保存到  List<List<string>>
/// 并调用 sql来刷新数据库
/// 
/// save all data in excle withe format  List<List<string>>
/// and update database
/// </summary>
public class local_data
{
    ExcelReaderListString c = new ExcelReaderListString();

    // 从excle 中读取的数据   data from excel
    List<List<string>> data_patient_information = null; // 所有病人的基本信息  TODO: 生成数据库中那种具有所有信息的listlist
    List<List<string>> data_patient_ors = null;
    List<List<string>> data_patient_icu = null;
    List<List<string>> data_arrangement = null;
    List<List<string>> data_arrange_specialite = null;
    //data regrouped  
    private List<List<string>> data_arrangement_format = null;
    private string data_json;

    bdd_functions bdd = new bdd_functions();
    dynamic data_dic = new ExpandoObject();

    public void load_data(HttpServerUtility Server)
    {
        //load from different file
        loadPatientInformation(Server, "patientInfos");  //patients2icu.xls
        loadArrangement(Server, "timeBlock");
        loadPatientOrs(Server, "patientOrs");  //patients2ors
        loadPatientIcu(Server, "patientICU");  //patients2icu.xls
        // update database
        updatePatientInformation();
        updatePatientDepartement();
        updatePatientIcu();

        //生成一个json文件  patientInfos patientICU   patientOrs timeBlock
        data_json = load_json();
        
        //regroup data
        former_arrangement();
    }

    void loadPatientInformation(HttpServerUtility Server, string FileName)
    {
        //C:/Users/c/source/repos/WebSite1/WebSite1/ 
        string path = Server.MapPath("./App_Data/" + FileName + ".csv");
        Data_patient_information = c.rowReadAll(path, 1);
    }

    void loadPatientOrs(HttpServerUtility Server, string FileName)
    {
        //C:/Users/c/source/repos/WebSite1/WebSite1/       patients2ors.xls   patients2blocks.xls
        string path = Server.MapPath("./App_Data/"+FileName+".csv");//xls
        Data_patient_ors = c.rowReadAll(path, 1);
     }

    void loadPatientIcu(HttpServerUtility Server, string FileName)
    {
        //C:/Users/c/source/repos/WebSite1/WebSite1/       patients2ors.xls   patients2blocks.xls
        string path = Server.MapPath("./App_Data/" + FileName + ".csv");//xls
        Data_patient_icu = c.rowReadAll(path, 1);
    }
    
    // time block 
    void loadArrangement(HttpServerUtility Server, string FileName)
    {
        String path = Server.MapPath("./App_Data/"+FileName+ ".csv");
        Data_arrangement = c.rowReadAll(path, 1);
        path = Server.MapPath("./App_Data/Specialites_chirurgicales.csv");
        Data_arrange_specialite = c.rowReadAll(path, 1);
    }

    //bdd 中更新病人的各种信息  TODO: 病人中新建 arrangement,根据arrangement来修改参数
    //update data of a patient
    void updatePatientDepartement()
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
        for (int salle = 0; salle < Data_arrangement.Count; salle++)
        {
            for (int day = 0; day < Data_arrangement[salle].Count; day++)
            {
                if (Data_arrangement[salle][day] != "")
                {
                    int arrangeNumber = Convert.ToInt32(Data_arrangement[salle][day].ToString());
                    for (int patient = 0; patient < Data_patient_ors[arrangeNumber-1].Count; patient++)
                    {
                        //如果某一个病人 patient 要在这个 timeblock 动手术
                        // if a patient have opration in that time block 
                        // update database
                        if (Data_patient_ors[arrangeNumber-1][patient] == "1")  
                        {
                            // 病人在 patient infomation 这个excle中的科室   和  最终做手术的科室不同时，将病人计算为做手术的科室的人
                            // if patient have different specialite in file excle patient information and file arrangement
                            // use the specailite in file arrangement
                            // TODO: to avoid this problem
                            bdd.update_patient_departement(patient + 1, Data_arrange_specialite[salle][day]);
                            int n = specialies.IndexOf(Data_arrange_specialite[salle][day]);
                            bdd.update_patient_specialty(patient + 1, Convert.ToInt32(n+1));
                            
                            //update patient operation time
                            Type t = typeof(bdd_functions);
                            object obj = Activator.CreateInstance(t);
                            MethodInfo method = t.GetMethod("update_patient_ors_day" + (day + 1).ToString());
                            object[] parametersArray = new object[] { patient + 1, 1 };
                            method.Invoke(bdd, parametersArray);

                            // if a patient have operation , set status with 1
                            bdd.update_patient_ors_status(patient + 1,1);

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
                //bdd.init_patient_info
                bdd.update_patient_specialty(patient+1, Convert.ToInt32(Data_patient_information[patient][0]));
                bdd.update_patient_departement(patient + 1,specialies[Convert.ToInt32(Data_patient_information[patient][0])-1]);
                bdd.update_patient_urgencyLevel(patient + 1, Convert.ToInt32(Data_patient_information[patient][1]));
                bdd.update_patient_waitingTime(patient + 1, Convert.ToInt32(Data_patient_information[patient][2]));
                bdd.update_patient_maxWaitingTime(patient + 1, Convert.ToInt32(Data_patient_information[patient][3]));
                // init the patient operation information
                bdd.init_patient_operation(patient + 1);
            }
        }
    }

    void updatePatientIcu()
    {
        for (int patient = 0; patient < data_patient_icu[0].Count-2; patient++)
        {
            for (int i = 0; i < 7; i++)
            {
                // 根据函数名字调用函数
                // call function dynamically
                Type t = typeof(bdd_functions);
                object obj = Activator.CreateInstance(t);
                MethodInfo method = t.GetMethod("update_patient_icu_day"+(i+1).ToString());
                int useOrNot = Convert.ToInt32(data_patient_icu[i][patient]);
                object[] parametersArray = new object[] {patient + 1, useOrNot};
                method.Invoke(bdd, parametersArray);
            }
        }
    }

    // TODO
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
        //重组表格的 blocks2or-days 的内容 开放但是没有病人的时候用 ouvert   不开放的为 ""  有病人的为一串字符串 (病人的编号)+，
        //Reorganize the contents of blocks2or-days of the form
        //Open but no patient --> 'ouvert'
        //Not open --> " "
        //open with patients-->(patient number) +,   exemple --> '1,2,3,'
        if (Data_arrangement_format == null)
        {
            Data_arrangement_format = new List<List<string>>(Data_arrangement.ToArray());
        }
        for (int salle = 0; salle < Data_arrangement.Count; salle++)
        {
            for (int day = 0; day < Data_arrangement[salle].Count; day++)
            {
                string all_patient = ""; 
                if (Data_arrangement[salle][day] != "")
                {
                    int arrange = Convert.ToInt32(Data_arrangement[salle][day].ToString()) - 1;
                    for (int patient = 0; patient < Data_patient_information.Count; patient++)
                    {
                        //如果某一个病人 patient 要在这个 timeblock 动手术
                        // if a patient have operation in this time block
                        if (Data_patient_ors[arrange][patient] == "1")
                        {
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