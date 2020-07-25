using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskJun.ViewModel;

namespace TaskJun.Model
{
    class ContainerClient
    {
        public ContainerClient()
        {
            Dictionary<int, List<ClientDay>> data = WorkFiles.GetTest();
            Update(data);
        }
        private List<Client> clients = new List<Client>();

        public ObservableCollection<Client> Clients
        {
            get
            {
                return new ObservableCollection<Client>(clients);
            }
        }
        public void Update(Dictionary<int, List<ClientDay>> data)
        {
            foreach (int day in data.Keys)
            {
                foreach (ClientDay clientDay in data[day])
                {
                    Client client = clients.Find(client_ => client_.User == clientDay.User);
                    if (client != null)
                        client.Update(clientDay, day);
                    else
                        clients.Add(new Client(clientDay, day));
                }
            }
        }

        public void AddClients(string[] FileNames) {
            Dictionary<int, List<ClientDay>> data = WorkFiles.OpenFiles(FileNames);
            Update(data);
        }
    }
}
