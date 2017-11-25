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
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace CarWash_WPF
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private static string connectionString = @"Data Source=localhost;Initial Catalog=carwash;User ID=root;Password=""""; charset=utf8";
        // private static string connectionString = "Server=185.26.122.48;Database=host1277275_nik;User Id=host1277275_nik;Password=123456789";
        private static MySqlConnection connection = new MySqlConnection(connectionString);

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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            const string programConst = "memes4life";
            string tbLogin = textboxLogin.Text;
            string tbPass = textboxPassword.Password.ToString();
            string selectedSalt = "";

            if (string.IsNullOrWhiteSpace(tbLogin) || string.IsNullOrWhiteSpace(tbPass))
            {
                MessageBox.Show("Вы заполнили не все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    connection.Open();
                    string query_salt = $"SELECT salt from users where `login` = '{tbLogin}'";
                    MySqlCommand cmnd_salt = new MySqlCommand(query_salt, connection);
                    object resSalt = cmnd_salt.ExecuteScalar();
                    selectedSalt = Convert.ToString(resSalt);

                    if (selectedSalt != "")
                    {
                        string firstStepCheck = GetSHA256Hash(tbPass);
                        string secondStepCheck = GetSHA256Hash(firstStepCheck + selectedSalt);
                        string finalStepCheck = GetSHA256Hash(secondStepCheck + programConst);

                        string query_check = $"SELECT COUNT(*) from users where `login` = '{tbLogin}' AND `password` = '{finalStepCheck}'";
                        MySqlCommand cmnd_check = new MySqlCommand(query_check, connection);
                        object count = cmnd_check.ExecuteScalar();
                        int loginResult = Convert.ToInt32(count);
                        if (loginResult == 1)
                        {
                            MessageBox.Show("Добро пожаловать в систему!", "Авторизация пройдена", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            MainWindow MW = new MainWindow();
                            MW.Show();
                            Hide();
                        }
                        else
                        {
                            MessageBox.Show("Пароль введен неверно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователя с таким логином не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void btnToSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUp SU = new SignUp();
            SU.Show();
            Hide();
        }
       
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //btnMinimize
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
