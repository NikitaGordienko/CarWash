using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Data;

namespace CarWash_WPF
{
    class Database //БЫЛ НЕ STATIC
    {
        // Test
        private static string connectionString = "Server=localhost;Database=carwash;User Id=root;Password=";
        //private static string connectionString = "Server=185.26.122.48;Database=host1277275_nik;User Id=host1277275_nik;Password=123456789";
        private static MySqlConnection connection = new MySqlConnection(connectionString);

        public static void ExecuteReader(string query) //НЕ НУЖЕН
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetName(i) + "\t");
                    }
                    Console.WriteLine();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader[i].ToString() + "\t");

                        }
                        Console.WriteLine();
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Application.Current.Shutdown();
            }
            finally
            {
                connection.Close();
            }
        }

        public static void ExecuteWriter(string query)
        {
            try
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    transaction.Rollback();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }

        }

        public static DataTable CreateDataTable(string query)
        {
            try
            {

                // Открываем подключение
                connection.Open();

                // Создаем команду
                MySqlCommand command = new MySqlCommand(query, connection);

                // Создаем объект DataAdapter
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                // Создаем объект DataTable (для работы с данными без подключения)
                DataTable dt = new DataTable();

                // Заполняем DataTable
                adapter.Fill(dt);

                return dt;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return null;
            }
            finally
            {
                connection.Close();
            }

        }

        public static string ChangeDateToDatabaseFormat(string originalDate)
        {
            string newDate = "";
            string tempYear = originalDate.Substring(6, 4);
            string tempMonth = originalDate.Substring(3, 2);
            string tempDay = originalDate.Substring(0, 2);
            newDate = tempYear + "-" + tempMonth + "-" + tempDay;
            return newDate;
        }

    }
}
