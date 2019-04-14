using System;
using System.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// bdd_functions 的摘要说明
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
}