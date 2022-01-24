using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAppFinal.Models;

namespace WpfAppFinal.ViewModels

{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        // отображение вводимых данных
        private string displayText;
        public string DisplayText
        {
            get => displayText;
            set
            {
                displayText = value;
                OnPropertyChanged();
            }
        }
        //результат отображается в верхней части
        private string result;
        public string Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddCommand { get; }
        private void OnAddCommandExecute(object p)
        {
            DisplayText += p.ToString();
        }

        private bool CanAddCommandExecute(object p)
        {
            return true;
        }
        public ICommand AdditionCommand { get; }
        private void OnAdditionCommandExecute(object p)
        {

            Result = new DataTable().Compute(displayText, null).ToString();
            DisplayText += p as string;
        }

        private bool CanAdditionCommandExecute(object p)
        {
            if (DisplayText != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ICommand SqrCommand { get; }
        private void OnSqrCommandExecute(object p)
        {
            var a = Convert.ToDouble(DisplayText);
            var b = Calculator.Sqr(a);
            Result = $"{b}";
            DisplayText = $"√({DisplayText}) = {b}";
        }

        private bool CanSqrCommandExecute(object p)
        {
            if (DisplayText != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ICommand ResetCommand { get; }
        private void OnResetCommandExecute(object p)
        {
            DisplayText = "0";
            Result = string.Empty;
        }

        private bool CanResetCommandExecute(object p)
        {
            return true;
        }
        public ICommand RemoveCommand { get; }
        private void OnRemoveCommandExecute(object p)
        {
            if (DisplayText.Length != 0)
            {
                DisplayText = DisplayText.Remove(DisplayText.Length - 1);
            }
            else
            {
                DisplayText = DisplayText.Insert(DisplayText.Length, "0");
            }
        }

        private bool CanRemoveCommandExecute(object p)
        {
            if (DisplayText != null)
                return true;
            else
                return false;
        }
        public ICommand SCommand { get; }
        private void OnSCommandExecute(object p)
        {
            if (DisplayText == "-")
            {
                DisplayText = DisplayText.Remove(0, 1);
            }
            else
            {
                DisplayText = "-" + DisplayText;
            }
        }
        private bool CanSCommandExecute(object p)
        {
            if (DisplayText != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public MainWindowViewModel()
        {
            AddCommand = new RelayCommand(OnAddCommandExecute, CanAddCommandExecute);
            ResetCommand = new RelayCommand(OnResetCommandExecute, CanResetCommandExecute);
            AdditionCommand = new RelayCommand(OnAdditionCommandExecute, CanAdditionCommandExecute);
            RemoveCommand = new RelayCommand(OnRemoveCommandExecute, CanRemoveCommandExecute);
            SCommand = new RelayCommand(OnSCommandExecute, CanSCommandExecute);
            SqrCommand = new RelayCommand(OnSqrCommandExecute, CanSqrCommandExecute);
        }
    }
}
