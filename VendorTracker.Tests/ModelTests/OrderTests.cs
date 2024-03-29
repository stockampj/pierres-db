using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendorTracker.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using MySql.Data.MySqlClient;

namespace VendorTracker.Tests
{
    [TestClass]
    public class OrderTest : IDisposable
    {
        public void Dispose()
        {
            Order.ClearAll();
        }
        public OrderTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=vendortracker_test;";
        }
        string name;
        string description;
        int price;
        string date;
        Order newOrder;

        [TestInitialize]
        public void Setup()
        {
            name = "BigMuffuginOrder";
            description ="SeeName";
            price = 30;
            date = "10/10/2019";
            newOrder = new Order(name,description,price,date);
        }
        
        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
        {
        // Arrange, Act
        Order firstOrder= new Order("name","Mow the lawn",32,"today");
        Order secondOrder = new Order("name","Mow the lawn",32,"today");

        // Assert
        Assert.AreEqual(firstOrder, secondOrder);
        }


        [TestMethod]
        public void GetAll_RetrunsEmptyListFromDataBase_OrderList()
        {
            List<Order> newList = new List<Order> {};
            List<Order> result = Order.GetAll();
            CollectionAssert.AreEqual(newList, result);
        }
        
        [TestMethod]
        public void Constructor_ConstructorBuildsInstance_Order()
        {
            Assert.AreEqual(newOrder.GetType(),typeof(Order));
        }
        [TestMethod]
        public void GetAll_ReturnsAllOrderInList_List()
        {
            List<Order> expectedList = new List<Order>{};
            List<Order> gotAll = Order.GetAll();
            CollectionAssert.AreEqual(gotAll,expectedList);
        }
        [TestMethod]
        public void ClearAll_ClearsAllOrdersInList_List()
        {
            List<Order> emptyList = new List<Order> {};
            Order.ClearAll();
            List<Order> gotAll = Order.GetAll();
            CollectionAssert.AreEqual(emptyList,gotAll);
        }
        [TestMethod]
        public void Find_AbleToFindElementById_Order()
        {
            newOrder.Save();
            int expectedID = 1;
            Order foundOrder = Order.Find(expectedID);
            Console.WriteLine(foundOrder.Title,foundOrder.Description,foundOrder.Id);
            Assert.AreEqual(newOrder,foundOrder);
        }
        [TestMethod]
        public void Save_SavesToDatabase_OrderList()
        {
            //Arrange

            //Act
            newOrder.Save();
            List<Order> result = Order.GetAll();
            List<Order> testList = new List<Order>{newOrder};

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }
    }
}