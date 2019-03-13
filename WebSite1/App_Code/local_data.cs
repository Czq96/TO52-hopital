using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using Newtonsoft.Json;


/// <summary>
/// local_data 的摘要说明
/// 未来将所有读取本地 excle 的代码放在这个类中
/// </summary>
public class local_data
{
    ExcelReaderListString c = new ExcelReaderListString();

    List<List<string>> data_patient = null;
    List<List<string>> data_arrangement = null;
    public List<List<string>> data_arrangement_format = null;

    public dynamic data_dic = new ExpandoObject();
    string data_json;

    List<List<int>> arrangement = null;
    public void load_data(HttpServerUtility Server)
    {
        //调用读取 excel 文件存到listlist string 中
        //生成一个json文件
        load_arrangement(Server);
        load_patient(Server);
        data_json = load_json();
        former_arrangement();
    }

    public List<List<string>> load_patient(HttpServerUtility Server)
    {
        // 读取.xls 文件将数据存在 data_patient 中； 尚未支持其他格式的表格文件
        string path = Server.MapPath("./App_Data/patients2blocks.xls");
        data_patient = c.rowReadAll(path, 1);//C:/Users/c/source/repos/WebSite1/WebSite1/
        return data_patient;
    }

    public List<List<string>> load_arrangement(HttpServerUtility Server)
    {
        String path2 = Server.MapPath("./App_Data/blocks2or-days.xls");
        data_arrangement = c.rowReadAll(path2, 1); //C:/Users/c/source/repos/WebSite1/WebSite1/blocks2or-days.xls   ..//
        return data_arrangement;
    }

    public string load_json()
    {
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
                            patient.id = p;
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
                    int arrange = Convert.ToInt32(data_arrangement[salle][day].ToString()) - 1;
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

}