using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Stylist
  {
    private int _id;
    private string _name

    public Stylist(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if(!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = this.GetId() == newStylist.GetId();
        bool nameEquality = this.GetName() == newStylist.GetName();
        return (idEquality && nameEquality);
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

    public static List<Stylist> GetAll()
    {
    List<Stylist> allStylists = new List<Stylist>{};

    SqlConnection conn = DB.Connection();
    SqlDataReader rdr = null;
    conn.Open();

    SqlCommand cmd = new SqlCommand("select * from clients;", conn);
    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      int clientId = rdr.GetInt32(0);
      string clientName = rdr.GetString(1);
      Stylist newStylist = new Stylist(clientName, clientId);
      allStylists.Add(newStylist);
    }

    if(rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
     return allStylists;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("insert into stylists (name) output inserted.id values (@StylistName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@StylistName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
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
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      sqlCommand cmd = new SqlCommand("DELETE from stylists WHERE id = @StylistId; DELETE from clients WHERE client_id = @StylistId;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();

      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close()
      }
    }
    public static Stylist Find(int id)
      {
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr = null;
        Conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);
        SqlParameter stylistIdParameter = new SqlParameter();
        stylistIdParameter.ParameterName = "@StylistId";
        stylistIdParameter.Value = id.ToString();
        cmd.Parameters.Add(stylistIdParameter);
        rdr = cmd.ExecuteReader();

        int foundStylistId = 0;
        string foundStylistName = null;

        while(rdr.Read())
        {
          foundStylistId = rdr.GetInt32(0);
          foundStylistName = rdr.GetString(1);
        }
        Stylist foundStylist = new Stylist(foundStylistName, foundStylistId);

        if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }
        return foundStylist;
      }

      public void Update(string newStylistName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stylists SET stylist_type = @newStylistName OUTPUT INSERTED.stylist_type WHERE id = @StylistId;", conn);

      SqlParameter newStylistParameter = new SqlParameter();
      newStylistParameter.ParameterName = "@NewStylist";
      newStylistParameter.Value = newStylist;
      cmd.Parameters.Add(newStylistParameter);


      SqlParameter StylistIdParameter = new SqlParameter();
      StylistIdParameter.ParameterName = "@StylistId";
      StylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(StylistIdParameter);
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
