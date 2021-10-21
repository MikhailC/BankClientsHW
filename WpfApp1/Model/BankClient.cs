﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using Prism.Mvvm;

namespace WpfApp1.Model
{
    public class BankClient:BindableBase,INotifyDataErrorInfo
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
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Phone)));
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(FirstName)));
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(LastName)));
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(SecondName)));
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(PassportNumber)));
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(PassportSeries)));
                    
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
                    SetProperty(ref _firstName, value);

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
                    SetProperty(ref _secondName, value);
                   
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

                    SetProperty(ref _lastName, value);

                }
            }
            
            
        }

        private string GetSecuredString(string line)
        {
            if (string.IsNullOrEmpty(line)) return "";
            return Accessor is Manager ? line : PasswordString;
        }

        public string PassportSeries
        {
            get => GetSecuredString(_passportSeries);
            set
            {
                
              if (Accessor is Manager)
                {
                    SetProperty(ref _passportSeries, value);

                }
            }
        }

        public string PassportNumber
        {
            get=>GetSecuredString(_passportNumber);
            set
            {
                if (Accessor is Manager)
                {
                    SetProperty(ref _passportNumber, value);
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
                    SetProperty(ref _phone, value);
                }
            }

        }

        
        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errors=new List<string>();
            if (string.IsNullOrEmpty(propertyName) || !HasErrors) 
                return null;
            if (propertyName == "FirstName" && string.IsNullOrWhiteSpace(FirstName))
                errors.Add("First name should be filled");
            if(propertyName == "SecondName" && string.IsNullOrWhiteSpace(SecondName))
                errors.Add("Second name should be filled");
            if (propertyName == "LastName" && string.IsNullOrEmpty(LastName))
                errors.Add("Last name should be filled");
            if(propertyName == "Phone" && Accessor is Operator && string.IsNullOrEmpty(Phone))
                errors.Add("Phone should be filled");

            return errors;
        }

        public bool HasErrors => string.IsNullOrWhiteSpace(FirstName) ||
                                 string.IsNullOrWhiteSpace(SecondName) ||
                                 string.IsNullOrWhiteSpace(LastName) ||
                                 (Accessor is Operator && string.IsNullOrWhiteSpace(Phone));
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        bool CheckValid()
        {
            if(string.IsNullOrWhiteSpace(FirstName)) ErrorsChanged!.Invoke(this, new DataErrorsChangedEventArgs("FirstName"));
            if(string.IsNullOrWhiteSpace(SecondName)) ErrorsChanged!.Invoke(this, new DataErrorsChangedEventArgs("SecondName"));
            if(string.IsNullOrWhiteSpace(SecondName)) ErrorsChanged!.Invoke(this, new DataErrorsChangedEventArgs("LastName"));
            if(Accessor is Operator && string.IsNullOrWhiteSpace(SecondName)) 
                ErrorsChanged!.Invoke(this, new DataErrorsChangedEventArgs("Phone"));
 
            
            return HasErrors;
        }
    }
}

  