using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using EntityFrameworkApplication._Database_mdfDataSetTableAdapters;
using Microsoft.Reporting.WinForms;
using OfficeOpenXml;
using OfficeOpenXml.Style;

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

        private bool _isReportViewerLoaded;

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ReloadTracksList();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ArtistNameTxt.Text) && !string.IsNullOrWhiteSpace(TrackNameTxt.Text))
            {
                AddNewTrack();
                ReloadTracksList();
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
            TracksList = Сontext.Tracks.ToList();
            Grid.ItemsSource = TracksList;
        }

        private void ReportViewerOnLoad(object sender, EventArgs eventArgs)
        {
            ReportDataSource reportDataSource = new ReportDataSource();
            _Database_mdfDataSet dataset = new _Database_mdfDataSet();

            dataset.BeginInit();
            reportDataSource.Name = "DataSet";
            reportDataSource.Value = dataset.Tracks;
            ReportViewer.LocalReport.DataSources.Add(reportDataSource);
            ReportViewer.LocalReport.ReportPath = "../../Report.rdlc";
            dataset.EndInit();

            TracksTableAdapter tracksTableAdapter = new TracksTableAdapter { ClearBeforeFill = true };

            tracksTableAdapter.Fill(dataset.Tracks);

            ReportViewer.RefreshReport();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GenerateExcel();
        }

        public void GenerateExcel()
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
    }
}