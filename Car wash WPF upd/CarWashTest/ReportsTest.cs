using System;
using CarWash_WPF;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarWashTest
{
    [TestClass]
    public class ReportsTest
    {
        [TestMethod]
        public void ExecuteQueryTestSuccess()
        {
            //string connectionString = "Server=185.26.122.48;Database=host1277275_nik;User Id=host1277275_nik;Password=123456789";
            //MySqlConnection connection = new MySqlConnection(connectionString);
            string query = $@"SELECT * FROM appointment"; //корректный запрос
            string actualStatus = Database.ExecuteWriter(query);
            string expectedStatus = "Execute-Success";
            Assert.AreEqual(expectedStatus, actualStatus, false, "Значения не совпадают. Тест не пройден.");
        }

        [TestMethod]
        public void ExecuteQueryTestFailure()
        {
            string connectionString = "Server=185.26.122.48;Database=databasewrong;User Id=host1277275_nik;Password=123456789"; //неверная строка подключения
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = $@"SELECT * FROM appointment"; 
            string actualStatus = Database.ExecuteWriter(query);
            string expectedStatus = "Execute-Success";
            Console.WriteLine(actualStatus);
            Assert.AreEqual(expectedStatus, actualStatus, false, "Значения не совпадают. Тест не пройден.");
        }

        [TestMethod]
        public void ChangeDateToDatabaseFormatSuccess()
        {
            string originalDate = "16.12.2017";
            string actualStatus = Database.ChangeDateToDatabaseFormat(originalDate);
            string expectedStatus = "2017-12-16";
            Assert.AreEqual(expectedStatus, actualStatus, false, "Значения не совпадают. Тест не пройден.");
        }
    }
}
