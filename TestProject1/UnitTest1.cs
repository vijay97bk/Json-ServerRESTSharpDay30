using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RESTSharpTestDay30;
using System;
using System.Collections.Generic;
using System.Net;

namespace TestProject1
{
    public class Employee
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Salary { get; set; }
    }
    public class Tests
    {
        RestClient client;
        [SetUp]
        public void Setup()
        {
            client = new RestClient(" http://localhost:3000/Employee");
        }
        /// <summary>
        /// Called when [calling list return employee list]. UC1
        /// </summary>
        [Test]
        public void OnCallingList_ReturnEmployeeList()
        {
            IRestResponse responce = GetEmployeeList();
            Assert.AreEqual(responce.StatusCode, HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(responce.Content);
            Assert.AreEqual(8, dataResponse.Count);

            foreach(Employee e in dataResponse)
            {
                Console.WriteLine("id" + e.id + "Name" + e.Name + "Salary" + e.Salary);
            }
        }
        private IRestResponse GetEmployeeList()
        {
            
        }
        [Test]
        public void GivenEmployee_OnPost_ShouldReturnAddedEmployee()
        {
            RestRequest request = new RestRequest("/Employee", Method.POST);
            JsonObject JObjectBody = new JsonObject();
            JObjectBody.Add("Name", "Chopper");
            JObjectBody.Add("Salary", "5000");
            request.AddParameter("application/json", JObjectBody, ParameterType.RequestBody);
            IRestResponse responce = client.Execute(request);

            //assert
            Assert.AreEqual(responce.StatusCode, HttpStatusCode.Created);
            Employee DataResponce = JsonConvert.DeserializeObject<Employee>(responce.Content);
            Assert.AreEqual("Chopper", DataResponce.Name);
            Assert.AreEqual("5000", DataResponce.Salary);
            Console.WriteLine(responce.Content);
        }
    }
}