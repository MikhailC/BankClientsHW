using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Commands;
using Prism.Mvvm;
using WpfApp1.Helper;
using WpfApp1.Model;

namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {
        public ObservableCollection<BankClient> Clients { get; } = new ObservableCollection<BankClient>();

        private  ComboBoxItem _selectedOperator;
        private object _currentClient;

        public  IOperator Operator
        {
            get=> Configuration.Instance.Operator;
            set
            {
                var tmp = Configuration.Instance.Operator;
                Configuration.Instance.Operator = value;
                SetProperty(ref tmp, value);
                
            }
        }

        public ComboBoxItem SelectedOperator
        {
            get => _selectedOperator;
            set
            {
                
                SetProperty(ref _selectedOperator, value);
                if (value.Content.Equals("Operator"))
                    Operator = Configuration.BankOperator;
                else
                    Operator =Configuration.BankManager;
                
 

            }
        }

        public ICommand AddNew => new AutoCanExecuteCommand(new DelegateCommand(() =>
        {
            BankClient bc = new BankClient();
            Clients.Add(bc);
            CurrentClient = bc;
        },()=>
            this.Operator is Manager
            ));

        public object CurrentClient
        {
            get => _currentClient;
            set => SetProperty<object>(ref _currentClient , value);
        }

        public MainWindowViewModel():base()
        {
     
            Operator = Configuration.BankManager;
           

            Randomizer.GetData(Clients, 100).ContinueWith((a)=>{});

            Operator = Configuration.BankOperator;

        }
    }
}