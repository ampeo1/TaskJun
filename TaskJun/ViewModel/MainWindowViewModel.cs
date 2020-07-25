using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using TaskJun.Model;


namespace TaskJun.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            Clients = new ObservableCollection<Client>(ContainerClient.GetClients());
        }
        public ObservableCollection<Client> Clients { get; set; }
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
                catch(NullReferenceException)
                {
                    return null;
                }
            }
        }

        public string[] Labels
        {
            get
            {
                try{
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


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
