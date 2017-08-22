using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVM_Matches
{
    //Этот файл мы создали вручную, результат - ручное создание команд. Будем использовать библиотеку MVVM Light чтобы упростить себе жизнь (упростить создание команд и упростить оповещение формы о том, что мы что-то измененили в коде (здесь это надо было бы сделать с помощью INotifyPropertyChanged))
    public class MainViewModel 
    {

        public class RelayCommand : ICommand
        {

            Action<object> _action;
            Func<bool> _canExecute;
            public RelayCommand(Action<object> action, Func<bool> canExecute)
            {
                _action = action;
                _canExecute = canExecute;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return _canExecute();
            }


            public void Execute(object parameter)
            {
                _action(parameter);
                //MessageBox.Show("Cmd Execute");
            }
        }
        public Match SelectedMatch {
            get;
            set;
        }

     

        public RelayCommand DoSmth { get; set; }
        //public List<Match>  Matches
        //{
        //    get
        //    {
        //        return _matches;
        //    }
        //}

        private void CreateMatches()
        {
            //_matches = new List<Match>()
            //{
            //    new Match(){HomeGoals=2, AwayGoals =2, HomeTeam="Man Unt", AwayTeam = "Man city"},
            //    new Match(){HomeGoals=2, AwayGoals =2, HomeTeam="Liverpool", AwayTeam = "Everton"}

            //};

            DoSmth = new RelayCommand(
                ButtonCLick, 
                () => true);
        }

        public void ButtonCLick(object param)
        {
            if(SelectedMatch.HomeTeam != "Man Unt")
            MessageBox.Show("Selected: " + SelectedMatch.HomeTeam);
        }
        public MainViewModel()
        {
            CreateMatches();
        }
    }
}
