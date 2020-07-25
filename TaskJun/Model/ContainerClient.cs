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
        public static List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            Dictionary<int, List<ClientDay>> data = WorkFiles.GetData();
            //Отсортировать по ключу 
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
            return clients;
        }
    }
}
