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
}