using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace CarWash_WPF
{
    /// <summary>
    /// Логика взаимодействия для ReportsWindow.xaml
    /// </summary>
    public partial class ReportsWindow : Window
    {
        private DataSet DS;
        DataTable ClientsByRegDateDT;
        DataTable AppointmentsByDateAndPriceDT;
        DataTable FeedbackByRateDT;

        List<string> collection = new List<string> { ">=", "=", "<=" };


        private static Excel.Application objApp;
        private static Excel._Workbook objBook;

        public ReportsWindow(DataSet MainWindowsDS)
        {
            InitializeComponent();
            DS = MainWindowsDS;
        }

        private void ReportFormWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DGClientsByDate.CanUserResizeColumns = false;
            DGClientsByDate.IsReadOnly = true;
            //DGClientsByDate.Columns[0].Header = "Номер клиента";
            //DGClientsByDate.Columns[1].Header = "Имя";
            //DGClientsByDate.Columns[2].Header = "Номер телефона";
            //DGClientsByDate.Columns[3].Header = "Email";
            //DGClientsByDate.Columns[4].Header = "Комментарий о клиенте";


            DGAppointmentsByDateAndPrice.CanUserResizeColumns = false;
            DGAppointmentsByDateAndPrice.IsReadOnly = true;
            //DGAppointments.Columns[0].Header = "Номер записи";
            //DGAppointments.Columns[1].Header = "Номер клиента";
            //DGAppointments.Columns[2].Header = "Время";
            //DGAppointments.Columns[3].Header = "Дата";
            //DGAppointments.Columns[4].Header = "Номер бокса";
            //DGAppointments.Columns[5].Header = "Класс автомобиля";
            //DGAppointments.Columns[6].Header = "Химчистка салона";
            //DGAppointments.Columns[7].Header = "Диагностика";
            //DGAppointments.Columns[8].Header = "Цена";

            DGFeedbackByRate.CanUserResizeColumns = false;
            DGFeedbackByRate.IsReadOnly = true;
            //DGFeedback.Columns[0].Header = "Номер записи";
            //DGFeedback.Columns[1].Header = "Оценка";
            //DGFeedback.Columns[2].Header = "Комментарий";


            ClientsByRegDateDT = DS.Tables[0];
            AppointmentsByDateAndPriceDT = DS.Tables[1];
            FeedbackByRateDT = DS.Tables[2];

            DGClientsByDate.ItemsSource = ClientsByRegDateDT.DefaultView;
            DGAppointmentsByDateAndPrice.ItemsSource = AppointmentsByDateAndPriceDT.DefaultView;
            DGFeedbackByRate.ItemsSource = FeedbackByRateDT.DefaultView;

            SignForPriceSort.ItemsSource = collection;
            SignForRateSort.ItemsSource = collection;

            datePickerAppointmentsFrom.IsEnabled = false;
            datePickerAppointmentsTo.IsEnabled = false;
            PriceBox.IsEnabled = false;
            SignForPriceSort.IsEnabled = false;

            SignForRateSort.SelectedIndex = 0; //
            SignForRateSort.SelectedIndex = 0; //

        }

        private void ShowClientByRegDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Проверка корректности выбранного промежутка дат
                if (datePickerForClientsFrom.SelectedDate > datePickerForClientsTo.SelectedDate)
                    throw new Exception("Выбран некорректный промежуток дат");

                // Преобразование дат к формату MySQL(гггг-мм-дд) с помощью метода в классе Database
                string startDate = Database.ChangeDateToDatabaseFormat(datePickerForClientsFrom.SelectedDate.ToString());
                string endDate = Database.ChangeDateToDatabaseFormat(datePickerForClientsTo.SelectedDate.ToString());

                // Создание запроса
                string showClientsByRegDateQuery = $@"SELECT * FROM client WHERE CLIENT_ID IN (SELECT CLIENT_ID FROM account WHERE REGISTRATION_DATE BETWEEN ""{startDate}"" AND ""{endDate}"")";

                // Заполнение элемента DataTable на основе запроса
                DataTable ClientsByRegDateDT = Database.CreateDataTable(showClientsByRegDateQuery);

                DS.Tables.Add(ClientsByRegDateDT);

                // Элемент DGV переопределяется в соответствии с новым источником
                DGClientsByDate.ItemsSource = ClientsByRegDateDT.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ShowAppointmentsByDateAndPrice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Переменные для формирования запроса
                string showAppointmentsByDateAndPriceQuery = "";               


                // Определения формата запроса (дата,цена)\(дата)\(цена)\(отсутствует)
                if (cbTurnOnDateSort.IsChecked == true & cbTurnOnPriceSort.IsChecked == true)
                {
                    // Проверка корректности выбранного промежутка дат и Преобразование дат к формату MySQL(гггг-мм-дд) с помощью метода в классе Database
                    if (datePickerAppointmentsFrom.SelectedDate > datePickerAppointmentsTo.SelectedDate)
                        throw new Exception("Выбран некорректный промежуток дат");
                    string startDate = Database.ChangeDateToDatabaseFormat(datePickerAppointmentsFrom.ToString());
                    string endDate = Database.ChangeDateToDatabaseFormat(datePickerAppointmentsTo.ToString());

                    //Проверка правильности ввода цены
                    string sign = SignForPriceSort.SelectedItem.ToString(); //SelectedItem
                    string price = PriceBox.Text;
                    if (int.Parse(PriceBox.Text.ToString()) < 0) 
                        throw new Exception("Недопустимая цена");
                    showAppointmentsByDateAndPriceQuery = $@"SELECT * FROM appointment WHERE APPOINTMENT_DATE BETWEEN ""{startDate}"" AND ""{endDate}"" AND PRICE {sign} {price}";
                }
                else if (cbTurnOnDateSort.IsChecked == true & cbTurnOnPriceSort.IsChecked == false)
                {
                    // Проверка корректности выбранного промежутка дат и Преобразование дат к формату MySQL(гггг-мм-дд) с помощью метода в классе Database
                    if (datePickerAppointmentsFrom.SelectedDate > datePickerAppointmentsTo.SelectedDate)
                        throw new Exception("Выбран некорректный промежуток дат");
                    string startDate = Database.ChangeDateToDatabaseFormat(datePickerAppointmentsFrom.ToString());
                    string endDate = Database.ChangeDateToDatabaseFormat(datePickerAppointmentsTo.ToString());
                    showAppointmentsByDateAndPriceQuery = $@"SELECT * FROM appointment WHERE APPOINTMENT_DATE BETWEEN ""{startDate}"" AND ""{endDate}""";
                }
                else if (cbTurnOnDateSort.IsChecked == false & cbTurnOnPriceSort.IsChecked == true)
                {
                    string sign = SignForPriceSort.SelectedItem.ToString(); //SelectedItem
                    string price = PriceBox.Text;
                    if (int.Parse(PriceBox.Text.ToString()) < 0) //проверка введенной цены
                        throw new Exception("Недопустимая цена");
                    showAppointmentsByDateAndPriceQuery = $@"SELECT * FROM appointment WHERE PRICE {sign} {price}";
                }
                else if (cbTurnOnDateSort.IsChecked == false & cbTurnOnPriceSort.IsChecked == false)
                {
                    showAppointmentsByDateAndPriceQuery = $@"SELECT * FROM appointment";
                }

                // Заполнение элемента DataTable на основе запроса
                AppointmentsByDateAndPriceDT = Database.CreateDataTable(showAppointmentsByDateAndPriceQuery);

                // Элемент DG переопределяется в соответствии с новым источником
                DGAppointmentsByDateAndPrice.ItemsSource = AppointmentsByDateAndPriceDT.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowFeedBackByRate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Переменные для формирования запроса
                string sign = SignForRateSort.SelectedItem.ToString();
                if (!(int.Parse(RateBox.Text.ToString()) >= 1 & int.Parse(RateBox.Text.ToString()) <= 5))
                    throw new Exception("Недопустимое значение рейтинга");

                string rating = RateBox.Text.ToString();
                string showFeedBackByRateQuery = $@"SELECT * FROM review WHERE VALUE {sign} {rating}";

                // Заполнение элемента DataTable на основе запроса
                FeedbackByRateDT = Database.CreateDataTable(showFeedBackByRateQuery);

                // Элемент DG переопределяется в соответствии с новым источником
                DGFeedbackByRate.ItemsSource = FeedbackByRateDT.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void cbTurnOnDateSort_Checked(object sender, RoutedEventArgs e)
        {
            datePickerAppointmentsFrom.IsEnabled = true;
            datePickerAppointmentsTo.IsEnabled = true;
        }

        private void cbTurnOnPriceSort_Checked(object sender, RoutedEventArgs e)
        {
            PriceBox.IsEnabled = true;
            SignForPriceSort.IsEnabled = true;
        }

        private void cbTurnOnDateSort_Unchecked(object sender, RoutedEventArgs e)
        {
            datePickerAppointmentsFrom.IsEnabled = false;
            datePickerAppointmentsTo.IsEnabled = false;
        }

        private void cbTurnOnPriceSort_Unchecked(object sender, RoutedEventArgs e)
        {
            PriceBox.IsEnabled = false;
            SignForPriceSort.IsEnabled = false;
        }



        private void FormClientsExcelReport_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            object[][] reportArray = CreateArrayFromDataTable(ClientsByRegDateDT);
            object[][] headingArray = CreateHeadingArrayFromDataTable(ClientsByRegDateDT);
            object[,] reportArray2D = CreateTwoDimensionalArrayFromStepArray(reportArray);
            object[,] headingArray2D = CreateTwoDimensionalArrayFromStepArray(headingArray);
            CreateExcelReport(headingArray2D, reportArray2D);
            this.IsEnabled = true;
        }

        private void FormAppointmentsExcelReport_Click(object sender, RoutedEventArgs e)
        {
            double max, min, avg;
            this.IsEnabled = false;
            object[][] reportArray = CreateArrayFromDataTable(AppointmentsByDateAndPriceDT);
            object[][] headingArray = CreateHeadingArrayFromDataTable(AppointmentsByDateAndPriceDT);
            object[,] reportArray2D = CreateTwoDimensionalArrayFromStepArray(reportArray);
            reportArray2D = ChangeStructure(reportArray2D);
            FindMaxMinAvgPrice(reportArray2D, out max, out min, out avg);
            object[] totalsArray = new object[] { "MAX:", max.ToString(), "MIN:", min.ToString(), "AVG:", avg.ToString() };
            object[,] headingArray2D = CreateTwoDimensionalArrayFromStepArray(headingArray);
            CreateExcelReport(headingArray2D, reportArray2D,totalsArray);
            this.IsEnabled = true;
        }

        private void FormFeedbackExcelReport_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            object[][] reportArray = CreateArrayFromDataTable(FeedbackByRateDT);
            object[][] headingArray = CreateHeadingArrayFromDataTable(FeedbackByRateDT);
            object[,] reportArray2D = CreateTwoDimensionalArrayFromStepArray(reportArray);
            object[,] headingArray2D = CreateTwoDimensionalArrayFromStepArray(headingArray);
            CreateExcelReport(headingArray2D,reportArray2D);
            this.IsEnabled = true;
        }

        //Методы работы с Excel
        private object[,] ChangeStructure(object[,] reportArray2D)
        {
            for (int i = 0; i < reportArray2D.GetLength(0); i++)
            {
                for (int j = 0; j < reportArray2D.GetLength(1); j++)
                {
                    if (reportArray2D[i, j].GetType() == typeof(DateTime))
                    {
                        DateTime tempDate = (DateTime)reportArray2D[i, j];
                        string tempDateString = tempDate.ToShortDateString();
                        reportArray2D[i, j] = tempDateString;
                    }
                    else
                    {
                        reportArray2D[i, j] = reportArray2D[i, j].ToString();
                    }

                }
            }

            return reportArray2D;
        }

        private object[][] CreateArrayFromDataTable(DataTable reportTable)
        {
            object[][] reportArray = new object[reportTable.Rows.Count][];

            for (int i = 0; i < reportTable.Rows.Count; i++)
            {
                reportArray[i] = reportTable.Rows[i].ItemArray;
            }
            return reportArray;
        }

        private object[][] CreateHeadingArrayFromDataTable(DataTable reportTable)
        {
            object[][] headingArray = new object[1][];
            headingArray[0] = new object[reportTable.Columns.Count];
            for (int i = 0; i < headingArray[0].Length; i++)
            {
                headingArray[0][i] = reportTable.Columns[i].Caption;
            }
            return headingArray;
        }

        private object[,] CreateTwoDimensionalArrayFromStepArray(object[][] reportArray)
        {
            object[,] reportArray2D = new object[reportArray.GetLength(0), reportArray[0].GetLength(0)];
            for (int i = 0; i < reportArray.GetLength(0); i++)
                for (int j = 0; j < reportArray[i].GetLength(0); j++)
                {
                    reportArray2D[i, j] = reportArray[i][j];
                }
            return reportArray2D;
        }

        private void CreateExcelReport(object[,] headingArray, object[,] reportArray)
        {
            Excel.Range range;
            Excel.Workbooks objBooks;
            Excel.Sheets objSheets;
            Excel._Worksheet objSheet;

            objApp = new Excel.Application();
            objBooks = objApp.Workbooks;
            objBook = objBooks.Add(Missing.Value);
            objSheets = objBook.Worksheets;
            objSheet = (Excel._Worksheet)objSheets.get_Item(1);

            range = objSheet.get_Range("A1", Missing.Value);
            range = range.get_Resize(headingArray.GetLength(0), headingArray.GetLength(1));
            range.set_Value(Missing.Value, headingArray);

            range = objSheet.get_Range("A2", Missing.Value);
            range = range.get_Resize(reportArray.GetLength(0), reportArray.GetLength(1));
            range.set_Value(Missing.Value, reportArray);
            objApp.Visible = true;
            objApp.UserControl = true;
        }

        private void CreateExcelReport(object[,] headingArray, object[,] reportArray, object[] totalsArray)
        {
            Excel.Range range;
            Excel.Workbooks objBooks;
            Excel.Sheets objSheets;
            Excel._Worksheet objSheet;

            objApp = new Excel.Application();
            objBooks = objApp.Workbooks;
            objBook = objBooks.Add(Missing.Value);
            objSheets = objBook.Worksheets;
            objSheet = (Excel._Worksheet)objSheets.get_Item(1);

            range = objSheet.get_Range("A1", Missing.Value);
            range = range.get_Resize(headingArray.GetLength(0), headingArray.GetLength(1));
            range.set_Value(Missing.Value, headingArray);

            range = objSheet.get_Range("A2", Missing.Value);
            range = range.get_Resize(reportArray.GetLength(0), reportArray.GetLength(1));
            range.set_Value(Missing.Value, reportArray);

            range = objSheet.get_Range("A"+ (reportArray.GetLength(0) + 2).ToString(), Missing.Value);
            range = range.get_Resize(totalsArray.GetLength(0));
            range.set_Value(Missing.Value, totalsArray);

            objApp.Visible = true;
            objApp.UserControl = true;
        }

        private void FindMaxMinAvgPrice(object[,] reportArray, out double max, out double min, out double avg)
        {
            max = double.MinValue;
            min = double.MaxValue;
            avg = 0;

            for (int i = 0; i < reportArray.GetLength(0); i++)
            {
                double price = double.Parse(reportArray[i, 8].ToString());
                if (price > max)
                {
                    max = price;
                }

                if (price < min)
                {
                    min = price;
                }
                avg += price;
            }

            avg = avg / reportArray.GetLength(0);          
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

    }
}
