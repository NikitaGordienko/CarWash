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
    public class Database
    {
        private static Database instance;

        private Database()
        {

        }

        public static Database GetInstance()
        {
            if (instance == null)
                instance = new Database();
            return instance;
        }
        // Строка подключения. Используется в качестве параметра для установки подключения.
        //private const string connectionString = "Server=localhost;Database=carwash;User Id=root;Password=;charset=utf8";
        private const string connectionString = "Server=185.26.122.48;Database=host1277275_nik;User Id=host1277275_nik;Password=123456789";
        // Объект MySQLConnection. Используется в метотдах в качестве объекта подключения
        private static MySqlConnection connection = new MySqlConnection(connectionString);

        // Выводит результат запроса в консоль. Не используется.
        public void ExecuteReader(string query) 
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
        
        // Исполняет запрос на UPDATE, DELETE и т. п. На вход получает строковую переменную с запросом
        public string ExecuteWriter(string query)
        {
            string executeStatus = "Execute";
            try // Блок TryCatchFinally №1. Основное назначение - проверить наличие подключения и в случае его отсутствия выдать сообщение об ошибке
            {
                connection.Open(); // Открытие подключения
                MySqlTransaction transaction = connection.BeginTransaction(); // Создание объекта транзакции
                MySqlCommand command = new MySqlCommand(query, connection); // Создание объекта команды на основе запроса и подключения
                command.Transaction = transaction; // Установление транзакции для исполнения текующей команды (запроса)
                try // Блок TryCatch №2. В случае, если невозможно выполнить запрос управление перейдет на блок Catch и откатит транзакцию
                {
                    command.ExecuteNonQuery(); // Исполнение команды (запроса)
                    transaction.Commit(); // Подтверждение транзакции
                    executeStatus+="-Success";
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // Вывод сообщения об ошибке
                    transaction.Rollback(); // Откат транзакции
                    executeStatus += "-Failure";
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // Вывод сообщения об ошибке
                executeStatus += "--Failure";
            }
            finally // Блок Finally исполняется в любом случае и закрывает подключение к базе данных
            {
                connection.Close();
            }
            return executeStatus;
        }

        // Исполнение запроса на SELECT и получение результата в качестве объекта DataTable
        public DataTable CreateDataTable(string query)
        {
            try
            {
                connection.Open(); // Открываем подключение
                MySqlCommand command = new MySqlCommand(query, connection); // Создание объекта команды на основе запроса и подключения
                MySqlDataAdapter adapter = new MySqlDataAdapter(command); // Создаем объект DataAdapter
                DataTable dt = new DataTable(); // Создаем объект DataTable (для работы с данными без подключения)
                adapter.Fill(dt);  // Заполняем DataTable
                return dt; // Возвращаемый объект

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // Вывод сообщения об ошибке
                Application.Current.Shutdown(); // Отключение приложения
                return null;
            }
            finally // Блок Finally исполняется в любом случае и закрывает подключение к базе данных
            {
                connection.Close();
            }

        }

        // в MySQL дата отображается в формате ГГГГ-ММ-ДД. В программе используется формат ДД.ММ.ГГГГ
        // Метод используется для обеспечения совместимости
        public string ChangeDateToDatabaseFormat(string originalDate)
        {
            string newDate = "";
            string tempYear = originalDate.Substring(6, 4);
            string tempMonth = originalDate.Substring(3, 2);
            string tempDay = originalDate.Substring(0, 2);
            newDate = tempYear + "-" + tempMonth + "-" + tempDay; // Формирование даты в новом формате
            return newDate;
        }

        // На вход получает объект таблицы, который соответствует DG и индекс выбранной строки в DG
        public string FormDeleteRecordQuery(DataTable editableTable, DataTable whereTable, int selectedIndex)
        {
            int selectedID = IdentifyID(editableTable, selectedIndex);
            string query = $"DELETE FROM {editableTable.TableName} WHERE {whereTable.TableName}_id = {selectedID};";
            return query;
        }

        public string FormChangeRecordQuery(DataTable editableTable, DataTable whereTable, int selectedIndex, bool withDate)
        {
            /*
             * Пока что запрос формируется на основе массива rowElements, который потом нужно будет передавать в метод в качестве параметра массива измененных значений
             * Так же пока не понятно, будет ли работать метод, если нарушить структуру ID в DG
             */
            object[] rowElements = editableTable.Rows[selectedIndex].ItemArray;
            int selectedID = IdentifyID(editableTable, selectedIndex);
            string query = $"UPDATE {editableTable.TableName} SET ";

            for (int i = 0; i < rowElements.Length - 1; i++)
            {
                if (withDate == true)
                {
                    if (i == 3) continue;
                    query += editableTable.Columns[i].ColumnName + "=" + "\"" + rowElements[i].ToString() + "\", ";
                }
                else
                {
                    query += editableTable.Columns[i].ColumnName + "=" + "\"" + rowElements[i].ToString() + "\", ";
                }

            }
            query += editableTable.Columns[rowElements.Length - 1].ColumnName + "=" + "\"" + rowElements[rowElements.Length - 1].ToString() + "\" "; // Запятой перед WHERE быть не должно
            query += $"WHERE {whereTable.TableName}_id = {selectedID};";

            return query;
        }

        // Метод предназначен для определения значения поля ID выделенной записи
        public int IdentifyID(DataTable editableTable, int selectedIndex)
        {
            int id = (int)editableTable.Rows[selectedIndex][0]; // Выбранная строка + Столбец №0
            return id;
        }

        // Используется для устранения противоречивовсти в списке запросов на исполнение.
        // Например изменение удаленной строки или изменение одной строки несколько раз
        // В приоритет взят запрос на DELETE, т.к либо пользователь удаляет измененную строку, либо пытается изменить уже удаленную строку.
        // Если формируется два запроса на UPDATE одной строке, то приоритет отдается последнему запросу
        public List<string> EliminateQueryInconsistency(List<string> queryList)
        {
            List<string> tempList = new List<string>();
            List<string> finalList = new List<string>();

            for (int i = 0; i < queryList.Count; i++)
            {
                string whereID = queryList[i].Substring(queryList[i].LastIndexOf(" ") + 1); // Выделение ID из запроса (WHERE example_id = ID)
                for (int j = i; j < queryList.Count; j++)
                {
                    if (queryList[j].Contains(whereID))
                    {
                        tempList.Add(queryList[j]); // TempList собирается все запросы содержащие определенный ID
                        queryList[j] = "NULL"; // Все собранные запросы в основном списке заменяются на NULL
                    }
                }
                tempList = DeleteDuplication(tempList); // Выделение одного приоритетного запроса в TempList
                finalList.AddRange(tempList); // Добавление запроса в финальный список для возвращения
                tempList.Clear();
            }

            finalList = RemoveNullItems(finalList); // Очистка от NULL строк
            return finalList;
        }

        // Вспомогательный метод для EliminateQueryInconsistency
        // Позволяет в рамках списка запросов для определенного ID выделить приоритеный запрос, а остальные удалить
        public List<string> DeleteDuplication(List<string> tempList)
        {
            int k = 0;
            while (tempList.Count != 1) // Цикл работает пока не останется один запрос на DELETE или UPDATE
            {
                if (tempList.Contains("DELETE")) // Если список содержит запрос на DELETE (соответственно этот запрос ставится в приоритет)
                {
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (!tempList[i].Contains("DELETE"))
                        {
                            tempList.RemoveAt(i); // Удаление всех записей не содержащих DELETE
                        }
                        else
                        {
                            k++; // Подсчет записей с DELETE
                        }

                    }
                }
                else // Если список не содержит ни одного запроса на DELETE
                    tempList.RemoveRange(0, tempList.Count - 1); // Выбирается самый последний запрос в списке, а остальные удаляются

                if (k > 1)
                {
                    tempList.RemoveRange(0, tempList.Count - 1); // Удаление дублированных записей DELETE, если такие имеются
                }
            }
            return tempList;
        }

        // Вспомогательный метод для EliminateQueryInconsistency
        // Удаляет все записи, содержащие строку NULL
        public List<string> RemoveNullItems(List<string> finalList)
        {
            while (finalList.Contains("NULL"))
            {
                finalList.Remove("NULL");
            }
            return finalList;
        }


    }
}
