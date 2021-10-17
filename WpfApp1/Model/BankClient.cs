using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using WpfApp1.Annotations;

namespace WpfApp1.Model
{
    public class BankClient:INotifyPropertyChanged
    {
        private string _passportSeries;
        private string _passportNumber;
        private string _phone;
        private string _firstName;

       

        private string _secondName;
        private string _lastName;
      
        private const string PasswordString = "********";

        static IOperator Accessor { get; set; }

      
        public BankClient()
        {
            Configuration.Instance.PropertyChanged += delegate(object args, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Operator")
                {
                    Accessor = Configuration.Instance.Operator;
                    OnPropertyChanged(nameof(Phone));
                    OnPropertyChanged(nameof(FirstName));
                    OnPropertyChanged(nameof(LastName));
                    OnPropertyChanged(nameof(SecondName));
                    OnPropertyChanged(nameof(PassportNumber));
                    OnPropertyChanged(nameof(PassportSeries));
                    
                }
            };
        }

        public BankClient(string passportSeries, string passportNumber, string phone, string firstName, string secondName,
            string lastName):this()
        {
            _passportSeries = passportSeries ?? throw new ArgumentNullException(nameof(passportSeries));
            _passportNumber = passportNumber ?? throw new ArgumentNullException(nameof(passportNumber));
            _phone = phone ?? throw new ArgumentNullException(nameof(phone));
            _firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            _secondName = secondName ?? throw new ArgumentNullException(nameof(secondName));
            _lastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (Accessor is Manager)
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        throw new ApplicationException("Name is mandatory.");
                    }
                    if (value == _firstName) return;
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecondName
        {
            get => _secondName;
            set
            { 
                
                if (Accessor is Manager)
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        throw new ApplicationException("Second name is mandatory.");
                    }
                    if (value == _secondName) return;
                    _secondName = value;
                    OnPropertyChanged();
                }
            }
            
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (Accessor  is Manager)
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        throw new ApplicationException("Last Name is mandatory.");
                    }
                    if (value == _lastName) return;
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PassportSeries
        {
            get=>Accessor is Manager?_passportSeries:PasswordString;
            set
            {
                if (Accessor is Manager)
                {
                    if (value == _passportSeries) return;
                    _passportSeries = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PassportNumber
        {
            get=>Accessor  is Manager?_passportNumber:PasswordString;
            set
            {
                if (Accessor is Manager)
                {
                    if (value == _passportNumber) return;
                    _passportNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Phone
        {
            get=>_phone;
            set
            {
                if (!string.IsNullOrEmpty(value) || Accessor is Manager)
                {
                    if (value == _phone) return;
                    _phone = value;
                    OnPropertyChanged();
                }
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

  