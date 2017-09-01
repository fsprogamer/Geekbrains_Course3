using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM.Model
{
    class Car : INotifyPropertyChanged
    {
        private string _model;
        private int _maxSpeed;
        private decimal _price;
        public string Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged("Model");
            }
        }
        public int MaxSpeed
        {
            get
            {
                return _maxSpeed;
            }
            set
            {
                _maxSpeed = value;
                OnPropertyChanged("MaxSpeed");
            }
        }
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
