using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskJun.Model
{
    class Client
    {
        public Client(ClientDay client, int day)
        {
            user = client.User;
            days.Add(day, client);
        }

        private string user;
        public string User {
            get
            {
                return user;
            }
        }

        private SortedDictionary<int, ClientDay> days = new SortedDictionary<int, ClientDay>();
        public SortedDictionary<int, ClientDay> Days
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
                return (int)Days.Values.Average(client => client.Steps);
            }
        }

        public string Color
        {
            get
            {
                string success = "#00CC00";
                string fail = "#FF0000";
                int temp = MaxSteps / AverageSteps;
                if ((float)MaxSteps / (float)AverageSteps * 100 > 120)
                {
                    return success;
                }
                if((float)MinSteps / (float)AverageSteps * 100 < 80)
                {
                    return fail;
                }
                return "#000";
            }
            
        }
        public void Update(ClientDay clientDay, int day)
        {
            days.Add(day, clientDay);
        }

        public List<int> GetSteps()
        {
            List<int> steps = Days.Values.Select(o => o.Steps).ToList();
            return steps;
        }

        public List<int> GetDays()
        {
            List<int> days = Days.Keys.ToList();
            return days;
        }
    }
}
