using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_ClientsEmptyAtFirst()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Client firstClient = new Client("david", 1);
      Client secondClient = new Client("david", 1);

      //Assert
      Assert.Equal(firstClient, secondClient);
    }

    [Fact]
    public void Test_Save_SavesClientToDatabase()
    {
      //Arrange
      Client testClient = new Client("david", 1);
      testClient.Save();

      //Act
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToClientObject()
    {
      //Arrange
      Client testClient = new Client("david", 1);
      testClient.Save();

      //Act
      Client savedClient = Client.GetAll()[0];
      Console.WriteLine(savedClient.GetStylistId() + "this is the test");
      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.Equal(testId, result);

    }

    [Fact]
    public void Test_Find_FindsClientInDatabase()
    {
      //Arrange
      Client testClient = new Client("david", 1);
      testClient.Save();
      Console.WriteLine("test client id: {0}",testClient.GetId());
      //Act
      Client foundClient = Client.Find(testClient.GetId());
      Console.WriteLine("found client id: {0}", foundClient.GetId());

      //Assert
      Assert.Equal(testClient, foundClient);
    }

    [Fact]
    public void Test_Update_UpdatesClientInDatabase()
    {
      //Arrange
      string name = "john";
      int stylistId = 1;
      Client testClient = new Client(name, stylistId);
      testClient.Save();
      string newName = "sarah";

      //Act
      testClient.Update(newName);

      Client result = new Client(testClient.GetName(), stylistId);
      Client newClient = new Client(newName, stylistId);

      //Assert
      Assert.Equal(newClient, result);
    }

    [Fact]
    public void Test_Delete_DeletesClientFromDatabase()
    {
      //Arrange
      string name1 = "john";
      Stylist testStylist1 = new Stylist(name1);
      testStylist1.Save();

      Client testClient1 = new Client("david", testStylist1.GetId());
      testClient1.Save();
      Client testClient2 = new Client("albert", testStylist1.GetId());
      testClient2.Save();

      //Act
      testClient1.Delete();
      List<Client> resultClients = testStylist1.GetClients();
      List<Client> testClientList = new List<Client> {testClient2};

      //Assert
      Assert.Equal(testClientList, resultClients);
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }

  }
}
