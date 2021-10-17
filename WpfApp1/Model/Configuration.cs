using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1.Annotations;

namespace WpfApp1.Model
{
    public sealed class Configuration:INotifyPropertyChanged
    {
        private IOperator _operator;
        private static Configuration? _instance;

        public static readonly IOperator BankOperator = new Operator();
        public static readonly IOperator BankManager = new Manager();


        public IOperator Operator
        {
            get => _operator;
            set { _operator = value; OnPropertyChanged();}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        static Configuration()
        {
            
        }

        private Configuration()
        {
            _operator = BankOperator;
        }

        public static Configuration Instance=>_instance ??= new Configuration();
        
    }
}