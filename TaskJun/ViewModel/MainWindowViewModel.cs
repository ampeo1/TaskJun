using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Microsoft.Win32;
using TaskJun.Model;


namespace TaskJun.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            Clients = new ContainerClient();
        }
        public ContainerClient Clients { get; set; }
        public Client selectedClient = null;
        public Client SelectedClient
        {
            get
            {
                return selectedClient;
            }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
                OnPropertyChanged("Steps");
                OnPropertyChanged("Labels");
            }
        }

        public SeriesCollection Steps
        {
            get
            {
                try
                {
                    return new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Количество шагов",
                            Values = new ChartValues<int>(selectedClient.GetSteps()),
                        }
                    };
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }

        public string[] Labels
        {
            get
            {
                try {
                    return selectedClient.GetDays().Select(day => day.ToString()).ToArray();
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }

        public Func<int, string> Formatter
        {
            get
            {
                return value => value.ToString();
            }
        }

        private RelayCommand saveUser;
        public RelayCommand SaveUser
        {
            get
            {
                return saveUser ?? (saveUser = new RelayCommand(obj =>
                {
                    if (selectedClient != null)
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Json file (*.json)|*.json";
                        saveFileDialog.FileName = selectedClient.User;
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            WorkFiles.SaveUser(selectedClient, saveFileDialog.FileName);
                        }
                        MessageBox.Show("Вы сохранили пользователя");
                    }
                    else
                    {
                        MessageBox.Show("Вы не выбрали пользователя, которого хотите сохранить");
                    }
                }));
            }
        }

        private RelayCommand addUsers;
        public RelayCommand AddUsers
        {
            get
            {
                return addUsers ?? (addUsers = new RelayCommand(obj =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Multiselect = true;
                    if(openFileDialog.ShowDialog() == true)
                    {
                        Clients.AddClients(openFileDialog.FileNames);
                        OnPropertyChanged("Clients");
                    }
                }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
