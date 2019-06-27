using System;
using System.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// bdd_functions for run the sql 
/// </summary>
public class bdd_functions
{
    MySqlConnection getConnection()
    {
        //MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnexion"].ConnectionString);
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=hopital;SslMode=none");
        return con;
    }

    //sert à executer la commande
    private void executeCmd(MySqlCommand cmd)
    {
        MySqlConnection con = getConnection();
        cmd.Connection = con;

        //Exécution de la commande
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //sert à executer la commande et retourne une variable INT
    private int executeCmdGetInt(MySqlCommand cmd)
    {
        MySqlConnection con = getConnection();
        cmd.Connection = con;

        //Exécution de la commande
        con.Open();
        int getInt = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();

        return getInt;
    }

    //sert à executer la commande et retourne une variable String
    private string executeCmdGetString(MySqlCommand cmd)
    {
        MySqlConnection con = getConnection();
        cmd.Connection = con;

        //Exécution de la commande et récupération du string
        con.Open();
        string getString = cmd.ExecuteScalar() as string;
        con.Close();

        return getString;
    }

    private DataTable CreateDataTable(MySqlCommand cmd)
    {
        MySqlConnection con = getConnection();
        cmd.Connection = con;
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        return dt;
    }
    public DataTable select_patient(int num)
    {
        DataTable d = new DataTable();
        using (MySqlCommand cmd = new MySqlCommand("select * from patient p where p.numero=@num"))
        {
            cmd.Parameters.AddWithValue("@num",num);
            d = CreateDataTable(cmd);
        }
        return d;
    }
    public void insert_patient(int num)
    {
        using (MySqlCommand cmd = new MySqlCommand("INSERT INTO patient VALUES(null, @num, \"\",\"\")"))
        {
            cmd.Parameters.AddWithValue("@num", num);
            executeCmd(cmd);
        }
    }
    public void update_patient_departement(int num, String dep)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET departement = @dep WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@dep", dep);
            executeCmd(cmd);
        }
    }
    public void update_patient_specialty(int num, int spe)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET specialty = @spe WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@spe", spe);
            executeCmd(cmd);
        }
    }
    public void update_patient_urgencyLevel(int num, int urgency)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET urgencyLevel = @urgency WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@urgency", urgency);
            executeCmd(cmd);
        }
    }
    public void update_patient_waitingTime(int num, int waitTime)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET waitingTime = @wt WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@wt", waitTime);
            executeCmd(cmd);
        }
    }
    public void update_patient_maxWaitingTime(int num, int maxTime)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET maxWaitingTime = @maxTime WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@maxTime", maxTime);
            executeCmd(cmd);
        }
    }

    public void update_patient_icu_day1(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET icuMonday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }

    public void update_patient_icu_day2(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET icuTuesday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }
    public void update_patient_icu_day3(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET icuWednesday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }
    public void update_patient_icu_day4(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET icuThursday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }
    public void update_patient_icu_day5(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET icuFriday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }
    public void update_patient_icu_day6(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET icuSaturday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }
    public void update_patient_icu_day7(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET icuSunday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }

    public void update_patient_ors_day1(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET orsMonday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }

    public void update_patient_ors_day2(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET orsTuesday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }
    public void update_patient_ors_day3(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET orsWednesday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }
    public void update_patient_ors_day4(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET orsThursday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }
    public void update_patient_ors_day5(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET orsFriday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }
    public void update_patient_ors_day6(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET orsSaturday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }
    public void update_patient_ors_day7(int num, int useOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET orsSunday = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", useOrNot);
            executeCmd(cmd);
        }
    }
    public void update_patient_ors_status(int num, int hasOrNot)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE patient SET hasOperation = @useOrNot WHERE patient.numero = @num "))
        {
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@useOrNot", hasOrNot);
            executeCmd(cmd);
        }
    }
    public int get_patients_number_by_specialty(int id)
    {
        int number;
        using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(patient.ID) FROM patient WHERE patient.specialty=@specialty"))
        {
            cmd.Parameters.AddWithValue("@specialty", id);
            number= executeCmdGetInt(cmd);
        }
        return number;
    }
    public int get_patients_number_hasOperation_by_specialty(int id)
    {
        int number;
        using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(patient.ID) FROM patient WHERE patient.specialty=@specialty AND patient.hasOperation =1"))
        {
            cmd.Parameters.AddWithValue("@specialty", id);
            number= executeCmdGetInt(cmd);
        }
        return number;
    }
    //bdd.init_patient_operation(patient + 1); 
    public void init_patient_operation(int num)
    {
        using (MySqlCommand cmd = new MySqlCommand("UPDATE `patient` SET `orsMonday` = '0', `orsTuesday` = '0', `orsWednesday` = '0', `orsThursday` = '0', `orsFriday` = '0', `orsSaturday` = '0', `orsSunday` = '0', `hasOperation` = '0' WHERE `patient`.`ID` = @num"))
        {
            cmd.Parameters.AddWithValue("@num", num);
            executeCmd(cmd);
}
    }
}
