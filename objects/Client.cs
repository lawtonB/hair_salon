using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _stylist_id;

    public Client(string Name, int Stylist_Id, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _stylist_id = Stylist_Id;
    }

    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = this.GetId() == newClient.GetId();
        bool nameEquality = this.GetName() == newClient.GetName();
        bool stylistIdEquality = this.GetStylistId() == newClient.GetStylistId();
        return (idEquality && nameEquality && stylistIdEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public int GetStylistId()
    {
      return _stylist_id;
    }

    public void SetStylistId(int newStylistId)
    {
      _stylist_id = newStylistId;
    }

    public static List<Client> GetAll()
    {
    List<Client> allClients = new List<Client>{};

    SqlConnection conn = DB.Connection();
    SqlDataReader rdr = null;
    conn.Open();

    SqlCommand cmd = new SqlCommand("select * from clients;", conn);
    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      int clientId = rdr.GetInt32(0);
      string clientName = rdr.GetString(1);
      int stylistId = rdr.GetInt32(2);
      Client newClient = new Client(clientName, stylistId, clientId);
      allClients.Add(newClient);
    }

    if(rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
     return allClients;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, stylist_id) OUTPUT INSERTED.id VALUES (@ClientName, @ClientId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@ClientName";
      nameParameter.Value = this.GetName();

      SqlParameter ClientIdParameter = new SqlParameter();
      ClientIdParameter.ParameterName = "@ClientId";
      ClientIdParameter.Value = this.GetStylistId().ToString();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(ClientIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE from clients WHERE id = @ClientId;", conn);

      SqlParameter ClientIdParameter = new SqlParameter();
      ClientIdParameter.ParameterName = "@ClientId";
      ClientIdParameter.Value = this.GetId();

      cmd.Parameters.Add(ClientIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
    public static Client Find(int id)
      {
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr = null;
        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);
        SqlParameter ClientIdParameter = new SqlParameter();
        ClientIdParameter.ParameterName = "@ClientId";
        ClientIdParameter.Value = id.ToString();
        cmd.Parameters.Add(ClientIdParameter);
        rdr = cmd.ExecuteReader();

        int foundClientId = 0;
        string foundClientName = null;

        while(rdr.Read())
        {
          foundClientId = rdr.GetInt32(0);
          foundClientName = rdr.GetString(1);
        }
        Client foundClient = new Client(foundClientName, foundClientId);

        if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }
        return foundClient;
      }

      public void Update(string newClientName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @newClientName OUTPUT INSERTED.name WHERE id = @ClientId;", conn);

      SqlParameter newClientParameter = new SqlParameter();
      newClientParameter.ParameterName = "@NewClientName";
      newClientParameter.Value = newClientName;
      cmd.Parameters.Add(newClientParameter);


      SqlParameter ClientIdParameter = new SqlParameter();
      ClientIdParameter.ParameterName = "@ClientId";
      ClientIdParameter.Value = this.GetId();
      cmd.Parameters.Add(ClientIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
