using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskJun.Model;

namespace TaskJun.ViewModel
{
    class MainWindowViewModel
    {
        public ObservableCollection<Client> Clients { get; set; }
        public MainWindowViewModel()
        {
            Clients = new ObservableCollection<Client>(ContainerClient.GetClients());
        }
    }
}
