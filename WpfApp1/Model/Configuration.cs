using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prism.Mvvm;

namespace WpfApp1.Model
{
    public sealed class Configuration:BindableBase
    {
        private IOperator _operator;
        private static Configuration? _instance;

        public static readonly IOperator BankOperator = new Operator();
        public static readonly IOperator BankManager = new Manager();


        public IOperator Operator
        {
            get => _operator;
            set { SetProperty(ref _operator , value); }
        }
        

        static Configuration()
        {
            
        }

        public  Configuration():base()
        {
            _operator = BankOperator;
            _instance = this;
        }

        public static Configuration Instance=>_instance ??= new Configuration();
        
    }
}