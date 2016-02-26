using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class StylistTest: IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_StylistsEmptyAtFirst()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("john");
      Stylist secondStylist = new Stylist("john");

      //Assert
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void Test_Save_SavesStylistToDataBase()
    {
      //Arrange
      Stylist testStylist = new Stylist ("john");
      Stylist otherStylist = new Stylist("sarah");
      testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      Assert.Equal(testList[0].GetName(), result[0].GetName());
    }

    [Fact]
    public void Test_Save_AssignsIdToStylistObject()
    {
      //Arrange
      Stylist testStylist = new Stylist("john");
      testStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsStylistInDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist ("john");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.Equal(testStylist, foundStylist);
    }

    [Fact]
    public void Test_Update_UpdatesStylistInDatabase()
    {
      //Arrange
      string StylistName = "John";
      Stylist testStylist = new Stylist(StylistName);
      testStylist.Save();
      string newStylistName = "Sarah";

      //Act
      testStylist.Update(newStylistName);

      string result = testStylist.GetName();

      //Assert
      Assert.Equal(newStylistName, result);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      // Client.DeleteAll();
    }
  }
}
