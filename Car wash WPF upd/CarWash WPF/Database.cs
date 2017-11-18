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
        // Test#2
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

        // На вход получает объект таблицы, который соответствует DG и индекс выбранной строки в DG
        public static string FormDeleteRecordQuery(DataTable editableTable, int selectedIndex)
        {
            // Определение системного ID записи
            int id = IdentifyID(editableTable, selectedIndex);
            // Формирование запроса
            string query = $"DELETE FROM {editableTable.TableName} WHERE {editableTable.TableName}_id = {id}";

            return query;
        }

        public static string FormChangeRecordQuery(DataTable editableTable, int selectedIndex)
        {
            /*
             * Пока что запрос формируется на основе массива rowElements, который потом нужно будет передавать в метод в качестве параметра массива измененных значений
             * Так же пока не понятно, будет ли работать метод, если нарушить структуру ID в DG
             */
            object[] rowElements = editableTable.Rows[selectedIndex].ItemArray;

            int id = IdentifyID(editableTable, selectedIndex);
            string query = $"UPDATE {editableTable.TableName} SET ";

            /* Тестирование проводилось для DGAppointments, в котором первые два столбца - первичные ключи, поэтому отсчет начинается с 2. (Так же нужно добавить как параметр метода)
             * Или придумать другой способ отличать значения PK и FK от других атрибутов (например по _id)
             */
            for (int i = 2; i < rowElements.Length; i++)
            {
                query += editableTable.Columns[i].ColumnName + "=" + rowElements[i].ToString()+ " ";
            }

            query += $"WHERE {editableTable.TableName}_id = {id}";

            return query;
        }

        public static int IdentifyID(DataTable editableTable, int selectedIndex)
        {
            int id = (int)editableTable.Rows[selectedIndex][0];
            return id;
        }

    }
}
