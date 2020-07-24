using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskJun.Model
{
    class Client
    {
        private string user;
        public string User {
            get
            {
                return user;
            }
        }

        private Dictionary<int, ClientDay> days = new Dictionary<int, ClientDay>();
        public Dictionary<int, ClientDay> Days
        {
            get
            {
                return days;
            }
        }

        public int MaxSteps {
            get
            {
                return Days.Values.Max(client => client.Steps);
            }
        }

        public int MinSteps {
            get
            {
                return Days.Values.Min(client => client.Steps);
            }
        }

        public int AverageSteps
        {
            get
            {
                return Days.Values.Sum(client => client.Steps) / Days.Count;
            }
        }

        public Client(ClientDay client, int day)
        {
            user = client.User;
            Days[day] = client;
        }
        public void Update(ClientDay clientDay, int day)
        {
            days[day] = clientDay;
        }
    }
}
