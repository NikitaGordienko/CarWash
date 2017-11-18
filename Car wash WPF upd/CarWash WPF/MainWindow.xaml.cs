using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarWash_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Стандартные SQL-запросы на вывод всех данных
        private readonly string ShowAllClientsQuery = @"SELECT * FROM client";
        private readonly string ShowAllAppointmentsQuery = @"SELECT * FROM appointment"; //`appointment_id`,`client_id`,`appointment_time`,`appointment_date`, `car_type`,`interior_cleaning`,`diagnostics`,`price`
        private readonly string ShowAllFeedbackQuery = @"SELECT * FROM review";
        private DataSet DS = new DataSet("Carwash");

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                // Создание объектов DataTable
                DataTable ClientsDT = Database.CreateDataTable(ShowAllClientsQuery);
                ClientsDT.TableName = "client";

                DataTable AppointmentsDT = Database.CreateDataTable(ShowAllAppointmentsQuery);
                AppointmentsDT.TableName = "appointment";

                DataTable FeedbackDT = Database.CreateDataTable(ShowAllFeedbackQuery);
                FeedbackDT.TableName = "review";

                // Добавление таблиц в объект DataSet
                DS.Tables.Add(ClientsDT);
                DS.Tables.Add(AppointmentsDT);
                DS.Tables.Add(FeedbackDT);


            }
            catch (Exception e)
            {
                string temp = e.Message;
                MessageBox.Show($"Невозможно подключиться к базе данных. \nПожалуйста, обратитесь к администратору.\nError: {e.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                //MainMenu main_menu = new MainMenu();
                //main_menu.Close();
            }
        }        

        private void MainMenuWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Попытка привести даты к формату дд.мм.гг
            //for (int i = 0; i < DS.Tables["Appointments"].Rows.Count; i++)
            //{
            //    DS.Tables[1].Rows[i].BeginEdit();
            //    DateTime tempDate = (DateTime)DS.Tables["Appointments"].Rows[i].Field<object>(3);
            //    string tempDateString = tempDate.ToShortDateString();
            //    MessageBox.Show($"{tempDateString}");
            //    DS.Tables["Appointments"].Rows[i].SetField(3, tempDateString);
            //    DS.Tables["Appointments"].Rows[i].AcceptChanges();
            //    MessageBox.Show($"{DS.Tables["Appointments"].Rows[i].Field<object>(3)}");
            //}

            // Объявление источников для DG
            DGClients.ItemsSource = DS.Tables[0].DefaultView;
            DGAppointments.ItemsSource = DS.Tables[1].DefaultView;
            DGFeedback.ItemsSource = DS.Tables[2].DefaultView;

            // Настройка отображения DGV элементов
            DGClients.CanUserResizeColumns = false;
            DGClients.IsReadOnly = true;
            DGClients.Columns[0].Header = "Номер клиента";
            DGClients.Columns[1].Header = "Имя";
            DGClients.Columns[2].Header = "Номер телефона";
            DGClients.Columns[3].Header = "Email";
            DGClients.Columns[4].Header = "Комментарий о клиенте";


            DGAppointments.CanUserResizeColumns = false;
            DGAppointments.IsReadOnly = true;
            //DGAppointments.Columns[0].Header = "Номер записи";
            //DGAppointments.Columns[1].Header = "Номер клиента";
            //DGAppointments.Columns[2].Header = "Время";
            //DGAppointments.Columns[3].Header = "Дата";
            //DGAppointments.Columns[4].Header = "Номер бокса";
            //DGAppointments.Columns[5].Header = "Класс автомобиля";
            //DGAppointments.Columns[6].Header = "Химчистка салона";
            //DGAppointments.Columns[7].Header = "Диагностика";
            //DGAppointments.Columns[8].Header = "Цена";

            DGFeedback.CanUserResizeColumns = false;
            DGFeedback.IsReadOnly = true;
            //DGFeedback.Columns[0].Header = "Номер записи";
            //DGFeedback.Columns[1].Header = "Оценка";
            //DGFeedback.Columns[2].Header = "Комментарий";
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            tabClients.IsSelected = true;
        }

        private void btnAppointments_Click(object sender, RoutedEventArgs e)
        {
            tabAppointments.IsSelected = true;
            //try
            //{
                
            //}
            //catch (ArgumentOutOfRangeException ex)
            //{
            //    MessageBox.Show(ex.Message, "ошибка");
            //}
        }

        private void btnFeedback_Click(object sender, RoutedEventArgs e)
        {
            tabFeedback.IsSelected = true;
            //try
            //{
                
            //}
            //catch (ArgumentOutOfRangeException ex)
            //{
            //    MessageBox.Show(ex.Message, "ошибка");
            //}
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            //tabReports.IsSelected = true;
            ReportsWindow rw = new ReportsWindow(DS);
            rw.ShowDialog();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
