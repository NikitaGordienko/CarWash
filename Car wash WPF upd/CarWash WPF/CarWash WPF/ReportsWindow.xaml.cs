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
            DGAppointmentsByDateAndPrice.CanUserResizeColumns = false;
            DGAppointmentsByDateAndPrice.IsReadOnly = true;
            DGFeedbackByRate.CanUserResizeColumns = false;
            DGFeedbackByRate.IsReadOnly = true;

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
            SignForPriceSort.IsEnabled = false;
            PriceBox.IsEnabled = false;
            cbDiagnosticsCheck.IsEnabled = false;
            cbInteriorCheck.IsEnabled = false;
            BoxBox.IsEnabled = false;
            ClassBox.IsEnabled = false;

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
                string appointmentFullQuery = "SELECT * FROM APPOINTMENT WHERE 1";
                string queryPartDate;
                string queryPartPrice;
                string queryPartDiagnostics;
                string queryPartInterior;
                string queryPartBox;
                string queryPartClass;

                if (cbTurnOnDateSort.IsChecked == true)
                {
                    //Проверка корректности выбранного промежутка дат и Преобразование дат к формату MySQL(гггг - мм - дд) с помощью метода в классе Database
                    if (datePickerAppointmentsFrom.SelectedDate > datePickerAppointmentsTo.SelectedDate)
                        throw new Exception("Выбран некорректный промежуток дат!");
                    string startDate = Database.ChangeDateToDatabaseFormat(datePickerAppointmentsFrom.ToString());
                    string endDate = Database.ChangeDateToDatabaseFormat(datePickerAppointmentsTo.ToString());
                    queryPartDate = $" AND APPOINTMENT_DATE BETWEEN '{startDate}' AND '{endDate}'";
                }
                else queryPartDate = ""; // или определить сверху

                if (cbTurnOnPriceSort.IsChecked == true)
                {
                    string sign = SignForPriceSort.SelectedItem.ToString();
                    string price = PriceBox.Text;
                    if (string.IsNullOrEmpty(price))
                        throw new Exception(@"Поле ""Цена"" не заполнено!");
                    bool isNumeric = int.TryParse(price, out int res);
                    if (res == 0)
                        throw new Exception(@"Поле ""Цена"" должно быть числовым!"); 
                    if (int.Parse(PriceBox.Text.ToString()) <= 0) 
                        throw new Exception("Недопустимая цена");
                    queryPartPrice = $" AND PRICE {sign} {price}";
                }
                else queryPartPrice = ""; 

                if (cbTurnOnDiagnostics.IsChecked == true)
                {
                    if (cbDiagnosticsCheck.IsChecked == true)
                        queryPartDiagnostics = $" AND DIAGNOSTICS = true";
                    else queryPartDiagnostics = $" AND DIAGNOSTICS = false";
                }
                else queryPartDiagnostics = "";

                if (cbTurnOnInterior.IsChecked == true)
                {
                    if (cbInteriorCheck.IsChecked == true)
                        queryPartInterior = $" AND DIAGNOSTICS = true";
                    else queryPartInterior = $" AND DIAGNOSTICS = false";
                }
                else queryPartInterior = "";

                if (cbTurnOnBox.IsChecked == true)
                {
                    string box = BoxBox.Text;
                    if (string.IsNullOrEmpty(box))
                        throw new Exception("Поле \"Номер бокса\" не заполнено!");
                    bool isNumeric = int.TryParse(box, out int res);
                    if (res == 0)
                        throw new Exception("Поле \"Номер бокса\" должно быть числовым!"); // если ввести 0, тоже ошибка 
                    if (int.Parse(BoxBox.Text.ToString()) <= 0)
                        throw new Exception("Неверный номер бокса");
                    queryPartBox = $" AND BOX_NUMBER = {box}";
                }
                else queryPartBox = "";

                if (cbTurnOnClass.IsChecked == true)
                {
                    string carClass = ClassBox.Text;
                    bool isNumeric = int.TryParse(carClass, out int res);
                    if (res != 0) throw new Exception("Класс автомобиля не может быть числом!");
                    queryPartClass = $" AND CAR_TYPE = '{carClass}'";
                }
                else queryPartClass = "";

                // Построение итогового запроса
                appointmentFullQuery += queryPartDate + queryPartPrice + queryPartDiagnostics + queryPartInterior + queryPartBox + queryPartClass;
                // Заполнение элемента DataTable на основе запроса
                AppointmentsByDateAndPriceDT = Database.CreateDataTable(appointmentFullQuery);
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
                bool isNumeric = int.TryParse(sign, out int res);
                if (res == 0)
                    throw new Exception("Поле \"Рейтинг\" должно быть числовым!");
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

        private void cbTurnOnDateSort_Unchecked(object sender, RoutedEventArgs e)
        {
            datePickerAppointmentsFrom.IsEnabled = false;
            datePickerAppointmentsTo.IsEnabled = false;
        }

        private void cbTurnOnPriceSort_Checked(object sender, RoutedEventArgs e)
        {
            PriceBox.IsEnabled = true;
            SignForPriceSort.IsEnabled = true;
        }

        private void cbTurnOnPriceSort_Unchecked(object sender, RoutedEventArgs e)
        {
            PriceBox.IsEnabled = false;
            SignForPriceSort.IsEnabled = false;
        }

        private void cbTurnOnDiagnostics_Checked(object sender, RoutedEventArgs e)
        {
            cbDiagnosticsCheck.IsEnabled = true;
        }

        private void cbTurnOnDiagnostics_Unchecked(object sender, RoutedEventArgs e)
        {
            cbDiagnosticsCheck.IsEnabled = false;
        }

        private void cbTurnOnInterior_Checked(object sender, RoutedEventArgs e)
        {
            cbInteriorCheck.IsEnabled = true;
        }

        private void cbTurnOnInterior_Unchecked(object sender, RoutedEventArgs e)
        {
            cbInteriorCheck.IsEnabled = false;
        }

        private void cbTurnOnBox_Checked(object sender, RoutedEventArgs e)
        {
            BoxBox.IsEnabled = true;
        }

        private void cbTurnOnBox_Unchecked(object sender, RoutedEventArgs e)
        {
            BoxBox.IsEnabled = false;
        }

        private void cbTurnOnClass_Checked(object sender, RoutedEventArgs e)
        {
            ClassBox.IsEnabled = true;       
        }

        private void cbTurnOnClass_Unchecked(object sender, RoutedEventArgs e)
        {
            ClassBox.IsEnabled = false;
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
            double max, min, avg, sum;
            this.IsEnabled = false;
            object[][] reportArray = CreateArrayFromDataTable(AppointmentsByDateAndPriceDT);
            object[][] headingArray = CreateHeadingArrayFromDataTable(AppointmentsByDateAndPriceDT);
            object[,] reportArray2D = CreateTwoDimensionalArrayFromStepArray(reportArray);
            reportArray2D = ChangeStructure(reportArray2D);
            FindMaxMinAvgSumPrice(reportArray2D, out max, out min, out avg, out sum);
            object[,] totalsArray = new object[,] { { "SUM:", sum.ToString() }, { "MAX:", max.ToString() }, { "MIN:", min.ToString() }, { "AVG:", avg.ToString("#.##") } };
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

        private void CreateExcelReport(object[,] headingArray, object[,] reportArray, object[,] totalsArray)
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

            range = objSheet.get_Range("H" + (reportArray.GetLength(0) + 3).ToString(), Missing.Value);
            range = range.get_Resize(totalsArray.GetLength(0), totalsArray.GetLength(1));
            range.set_Value(Missing.Value, totalsArray);

            objApp.Visible = true;
            objApp.UserControl = true;
        }

        private void FindMaxMinAvgSumPrice(object[,] reportArray, out double max, out double min, out double avg, out double sum)
        {
            max = double.MinValue;
            min = double.MaxValue;
            sum = 0;
            double price;

            for (int i = 0; i < reportArray.GetLength(0); i++)
            {
                price = double.Parse(reportArray[i, 8].ToString());
                if (price > max)
                {
                    max = price;
                }

                if (price < min)
                {
                    min = price;
                }
                sum += price;
            }

            avg = sum / reportArray.GetLength(0);
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
