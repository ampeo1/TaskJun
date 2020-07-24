using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskJun.Model;

namespace TaskJun.ViewModel
{
    class WorkFiles
    {
        public static Dictionary<int, List<ClientDay>> GetData()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "//TestData");
            string pattern = @"^day(\d+).json$";
            Dictionary<int, List<ClientDay>> data = new Dictionary<int, List<ClientDay>>();
            foreach (FileInfo file in directoryInfo.GetFiles("day*.json"))
            {
                if (Regex.IsMatch(file.Name, pattern, RegexOptions.IgnoreCase))
                {
                    int day = int.Parse(file.Name.Substring(3, file.Name.Length - 8)); //3 = day , .json + day = 8
                    string clients;
                    using (FileStream fileStream = File.OpenRead(file.FullName))
                    {
                        byte[] array = new byte[fileStream.Length];
                        fileStream.Read(array, 0, array.Length);
                        clients = Encoding.UTF8.GetString(array);
                    }
                    data[day] = JsonConvert.DeserializeObject<List<ClientDay>>(clients);
                }
            }
            return data;
        }
    }
}
