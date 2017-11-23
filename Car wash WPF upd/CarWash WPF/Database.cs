using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Controls;

namespace CarWash_WPF
{
    class Database //БЫЛ НЕ STATIC
    {
        // Test
        // Test#2
        private static string connectionString = "Server=localhost;Database=carwash;User Id=root;Password=;charset=utf8";
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
        public static string FormDeleteRecordQuery(DataTable editableTable, DataTable whereTable, int selectedIndex)
        {
            int selectedID = IdentifyID(editableTable, selectedIndex);
            string query = $"DELETE FROM {editableTable.TableName} WHERE {whereTable.TableName}_id = {selectedID};";
            return query;
        }

        public static string FormChangeRecordQuery(DataTable editableTable, DataTable whereTable, int selectedIndex, bool withDate)
        {
            /*
             * Пока что запрос формируется на основе массива rowElements, который потом нужно будет передавать в метод в качестве параметра массива измененных значений
             * Так же пока не понятно, будет ли работать метод, если нарушить структуру ID в DG
             */
            object[] rowElements = editableTable.Rows[selectedIndex].ItemArray;
            int selectedID = IdentifyID(editableTable, selectedIndex); 
            string query = $"UPDATE {editableTable.TableName} SET ";

            for (int i = 0; i < rowElements.Length-1; i++) 
            {
                if (withDate == true)
                {
                    if (i == 3) continue;
                    query += editableTable.Columns[i].ColumnName + "=" + "\"" + rowElements[i].ToString() + "\", ";
                }
                else
                {
                    query += editableTable.Columns[i].ColumnName + "=" + "\"" + rowElements[i].ToString()+ "\", ";
                }
                
            }
            query += editableTable.Columns[rowElements.Length - 1].ColumnName + "=" + "\"" + rowElements[rowElements.Length - 1].ToString() + "\" "; // Запятой перед WHERE быть не должно
            query += $"WHERE {whereTable.TableName}_id = {selectedID};";

            return query;
        }

        public static int IdentifyID(DataTable editableTable, int selectedIndex)
        {
            int id = (int)editableTable.Rows[selectedIndex][0];
            return id;
        }

        // В приоритет взят запрос на DELETE, т.к либо пользователь удаляет измененную строку, либо пытается изменить уже удаленную строку. - V
        // Если формируется два запроса на UPDATE одной строке, то приоритет отдается последнему запросу - X
        public static List<string> EliminateQueryInconsistency(List<string> queryList)
        {
            List<string> tempList = new List<string>();
            List<string> finalList = new List<string>();
            int y = queryList.Count;
            for (int i = 0; i < y; i++)
            { 
                string whereID = queryList[i].Substring(queryList[i].LastIndexOf(" ") + 1);
                for (int j = i; j < y; j++)
                {
                    if (queryList[j].Contains(whereID))
                    {
                        tempList.Add(queryList[j]);
                        queryList.RemoveAt(j);
                    }
                }
                tempList = DeleteDeduplication(tempList);
                finalList.AddRange(tempList);
                tempList = null;
            }

            return finalList;
        }

        public static List<string> DeleteDeduplication(List<string> tempList)
        {
            int k = 0;
            while(tempList.Count != 1)
            {
                if (tempList.Contains("DELETE"))
                {
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (!tempList[i].Contains("DELETE"))
                        {
                            tempList.RemoveAt(i);
                        }
                        else
                        {
                            k++;
                        }

                    }
                }
                else
                    tempList.RemoveRange(0, tempList.Count - 1);

                if (k>1)
                {
                    tempList.RemoveRange(0, tempList.Count - 1);
                }
            }
            return tempList;
        }


    }
}
