using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
        private readonly Configuration _configuration;
        public ObservableCollection<BankClient> Clients { get; } = new ObservableCollection<BankClient>();

        private BankClient? currentLine;

        private  ComboBoxItem _selectedOperator;
        private BankClient? _currentClient;

        public  IOperator Operator
        {
            get=> _configuration.Operator;
            set
            {
                var tmp = _configuration.Operator;
                _configuration.Operator = value;
                SetProperty(ref tmp, value);
                OnPropertyChanged(new PropertyChangedEventArgs("IsReadOnly"));
                
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
        
        public bool IsReadOnly=>!(Operator is Manager);
        
        
        #region Commands

        public ICommand AddNew => new AutoCanExecuteCommand(new DelegateCommand(() =>
        {
            CurrentLine = new BankClient();
        },()=>
            this.Operator is Manager
            ));

        public ICommand SaveClient =>
            new AutoCanExecuteCommand(new DelegateCommand(() => { Clients.Add(CurrentLine);
                    CurrentClient = CurrentLine;
                }, 
              ()=> CurrentLine is not null&&CurrentLine!=CurrentClient&&!CurrentLine.HasErrors ));

        public ICommand DeleteClient =>
            new AutoCanExecuteCommand(
                new DelegateCommand(
                    () =>
                    {
                        Clients.Remove(CurrentClient);
                        CurrentLine = null;
                    },
                    () => CurrentClient is not null&&Operator is Manager&&CurrentClient==CurrentLine));
        public ICommand GridSelectionChanged => new DelegateCommand<SelectionChangedEventArgs>((SelectionChangedEventArgs e) =>
        {
            
            var g = e.Source as DataGrid;
            if (g.SelectedItem is not null)
            {
                g.ScrollIntoView(g.SelectedItem);
                CurrentLine = g.SelectedItem as BankClient;
            }
        });
        #endregion 
        public BankClient? CurrentClient
        {
            get => _currentClient;
            set => SetProperty(ref _currentClient , value);
        }

        public BankClient? CurrentLine
        {
            get => currentLine;
            set => SetProperty(ref currentLine , value);
        }

        public MainWindowViewModel(Configuration configuration):base()
        {
            _configuration = configuration;

           //Operator = Configuration.BankManager;
           

            Randomizer.GetData(Clients, 100).ContinueWith((a)=>{});

           // Operator = Configuration.BankOperator;

        }
    }
}