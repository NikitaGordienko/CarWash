using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Cryptography; //
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace CarWash_WPF
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }
     
        private static string connectionString = @"Data Source=localhost;Initial Catalog=carwash;User ID=root;Password=""""; charset=utf8";
        //private static string connectionString = "Server=185.26.122.48;Database=host1277275_nik;User Id=host1277275_nik;Password=123456789";
        private static MySqlConnection connection = new MySqlConnection(connectionString);

        private static Random random = new Random();
        public static string GenerateSalt(int length)   //Метод создания соли
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        // Метод создания хешированной алгоритмом SHA-256 строки
        public static string GetSHA256Hash(string input)
        {
            SHA256 newSHA256 = SHA256.Create();
            byte[] array = Encoding.UTF8.GetBytes(input); //перевод строки в массив байтов   
            byte[] hashedArray = newSHA256.ComputeHash(array);    //хеширование алгоритмом SHA256
            StringBuilder hexStr = new StringBuilder(hashedArray.Length * 2); //Builder собирает байты в строку
            foreach (byte b in hashedArray)
                hexStr.AppendFormat("{0:x2}", b);
            return hexStr.ToString(); //возвращает шестнадцатеричную строку -> результат хеширования
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            const string programConst = "memes4life"; //Константа программы для последнего этапа хеширования
            string tbLogin = textLogin.Text;
            string tbPassword = textPassword.Password.ToString();
            string tbFio = textFio.Text;
            string tbEmail = textEmail.Text;
            string hashFirstStep; //Первый этап хеширования --> SHA256(пароль)
            string hashSecondStep; //Второй этап хеширования --> SHA256(SHA256(пароль)+соль)
            string hashFinalStep; // итоговый кодированный пароль, заносимый в базу

            if (string.IsNullOrWhiteSpace(tbLogin) || string.IsNullOrWhiteSpace(tbPassword) || string.IsNullOrWhiteSpace(tbFio) || string.IsNullOrWhiteSpace(tbEmail))
            {
                MessageBox.Show("Вы заполнили не все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    connection.Open();
                    string query_s = $"SELECT COUNT(*) FROM users WHERE login = '{tbLogin}'";
                    MySqlCommand cmnd_s = new MySqlCommand(query_s, connection);
                    object count = cmnd_s.ExecuteScalar();
                    int res = Convert.ToInt32(count);
                    if (res == 0)
                    {
                        if (tbPassword.Length >= 8)
                        {   // проверка паттерна при вводе поля "e-mail"
                            if (Regex.IsMatch(tbEmail, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$") && Regex.IsMatch(tbFio, @"^[a-яA-Я''-'\s]{1,50}$"))
                            {
                                string salt = GenerateSalt(10);  // Генерация соли
                                hashFirstStep = GetSHA256Hash(tbPassword);
                                hashSecondStep = GetSHA256Hash(hashFirstStep + salt);
                                hashFinalStep = GetSHA256Hash(hashSecondStep + programConst);

                                string query = $"INSERT INTO users (login, password, name, email, user_type, salt) VALUES ('{tbLogin}', '{hashFinalStep}', '{tbFio}', '{tbEmail}', '1', '{salt}')"; //user_type - 1, т.к. пока нет разделения администраторских учетных записей
                                MySqlCommand cmnd = new MySqlCommand(query, connection);
                                cmnd.ExecuteNonQuery(); // Выполнение запроса
                                MessageBox.Show("Регистрация пройдена!");
                                LoginPage log_p = new LoginPage();
                                log_p.Show();   
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Проверьте правильность ввода!", "Ошибка при регистрации!", MessageBoxButton.OK, MessageBoxImage.Warning); //может быть доработано
                            }
                        }
                        else
                        {
                            MessageBox.Show("В пароле должно быть не менее 8 символов", "Ошибка при регистрации!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка при регистрации!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); //Скрыть от пользователя
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
