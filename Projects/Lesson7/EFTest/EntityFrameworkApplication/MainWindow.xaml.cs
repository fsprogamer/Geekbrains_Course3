using EntityFrameworkApplication.Model;
using System.Collections.Generic;
using System.Linq;
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
    }
}