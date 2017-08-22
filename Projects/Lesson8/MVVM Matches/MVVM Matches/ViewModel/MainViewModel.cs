using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Windows;

namespace MVVM_Matches.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        

        IMatchRepository _repo = new MatchRepository();
        public List<Match> Matches
        {
            get
            {
                return _repo.Matches;
            }
        }
        public string Name { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public MainViewModel(IMatchRepository repo)
        {
            _repo = repo;
            //Здесь возможно прописывать вещи, которые будут делаться при тестовых прогонах программы и при реальном запуске (то есть если мы запускаем Debug из VS или VS Blend программа определяет это делает тестовый прогон)
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            SaveCommand = new RelayCommand( SaveCommandAction);
        }

        private void SaveCommandAction()
        {
            MessageBox.Show(" Save executed");
        }
    }
}