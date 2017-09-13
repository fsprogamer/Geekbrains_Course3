using EntityFrameworkApplication.DataSetTableAdapters;
using EntityFrameworkApplication.Model;
using Microsoft.Reporting.WinForms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows;


namespace EntityFrameworkApplication
{
    public partial class MainWindow : Window
    {
        private SqlLocalDbContext Сontext;
        public List<Track> TracksList;
        public MainWindow()
        {
            InitializeComponent();
            Сontext = new SqlLocalDbContext();

            ReportViewer.Load += ReportViewerOnLoad;
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ReloadTracksList();
        }
        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ArtistNameTxt.Text) && !string.IsNullOrWhiteSpace(TrackNameTxt.Text))
            {
                AddNewTrack();
                ReloadTracksList();//
            }
        }
        private void AddNewTrack()
        {
            Сontext.Tracks.Add(
                new Track
                {
                    ArtistName = ArtistNameTxt.Text,
                    TrackName = TrackNameTxt.Text
                });
            Сontext.SaveChanges();
        }
        private void ReloadTracksList()
        {
            TracksList = Сontext.Tracks.ToList();//
            Grid.ItemsSource = TracksList;//

            //Сontext.Tracks.Load();
            //Grid.ItemsSource = Сontext.Tracks.Local.ToBindingList();            
        }

        private void ReportViewerOnLoad(object sender, EventArgs eventArgs)
        {
            ReportDataSource reportDataSource = new ReportDataSource();
            DataSet dataset = new DataSet();
            dataset.BeginInit();
            reportDataSource.Name = "DataSet";
            reportDataSource.Value = dataset.Tracks;
            ReportViewer.LocalReport.DataSources.Add(reportDataSource);
            ReportViewer.LocalReport.ReportPath = "../../Report1.rdlc";
            dataset.EndInit();
            TracksTableAdapter tracksTableAdapter = new TracksTableAdapter { ClearBeforeFill = true };
            tracksTableAdapter.Fill(dataset.Tracks);
            ReportViewer.RefreshReport();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {

                //Создаем лист   
                ExcelWorksheet Worksheet = excelPackage.Workbook.Worksheets.Add("Trakcs");
                //Загружаем БД на лист, начиная с ячейки А2     
                Worksheet.Cells["A2"].LoadFromCollection(Сontext.Tracks);
                Worksheet.Cells["A1"].Value = "Id";
                Worksheet.Cells["B1"].Value = "Artist";
                Worksheet.Cells["C1"].Value = "Name";
                Worksheet.Cells.AutoFitColumns();
                //Изменяем стиль всего диапозона ячеек (первый ряд)   
                using (ExcelRange range = Worksheet.Cells["A1:XFD1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.Fill.PatternType = ExcelFillStyle.LightGray;

                }
                excelPackage.SaveAs(new System.IO.FileInfo("test.xlsx"));

            }
        }

        private void Button_Click_ComInterop(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelApp;
            Microsoft.Office.Interop.Excel.Workbook book;
            Microsoft.Office.Interop.Excel.Worksheet sheet;
            Microsoft.Office.Interop.Excel.Range range;

            try
            {
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Visible = true;

                book = (Microsoft.Office.Interop.Excel.Workbook)(excelApp.Workbooks.Add(Missing.Value));
                sheet = (Microsoft.Office.Interop.Excel.Worksheet)book.ActiveSheet;

                sheet.Cells[1, 1] = "Track Id";
                sheet.Cells[1, 2] = "Author";
                sheet.Cells[1, 3] = "Name";

                sheet.get_Range("A1", "C1").Font.Bold = true;
                sheet.get_Range("A1", "C1").VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                string[,] Tracks = new string[Сontext.Tracks.Count(),3];
                var tracks = (from t in Сontext.Tracks where t.TrackId == 1 select t);

                for (int i = 1; i < Сontext.Tracks.Count() + 1; i++)
                {
                    Track Item = Сontext.Tracks.First(j => j.TrackId == i);

                    Tracks[i - 1, 0] = Item.TrackId.ToString();
                    Tracks[i - 1, 1] = Item.ArtistName.ToString();
                    Tracks[i - 1, 2] = Item.TrackName.ToString();
                }
                sheet.get_Range("A2", "C4").Value2 = Tracks;

                range = sheet.get_Range("D2", "D6");
                range.Formula = "=B2 & \" \" & C2";

                ////AutoFit columns A:D.
                range = sheet.get_Range("A1", "D1");
                range.EntireColumn.AutoFit();

            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }

        }
    }
}