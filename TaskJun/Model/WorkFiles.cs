using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using TaskJun.Model;

namespace TaskJun.ViewModel
{
    class WorkFiles
    {
        private static Dictionary<int, List<ClientDay>> GetData(FileInfo[] fileInfos)
        {
            Dictionary<int, List<ClientDay>> data = new Dictionary<int, List<ClientDay>>();
            string pattern = @"^day(\d+).json$";
            foreach (FileInfo file in fileInfos)
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
                    try
                    {
                        data[day] = JsonConvert.DeserializeObject<List<ClientDay>>(clients);
                    }
                    catch (JsonException)
                    {
                        MessageBox.Show($"Проблемы с файлом {file.Name}");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Имя файла должно быть вида \"day(номер дня).json\"");
                    return data;
                }
            }
            return data;
        }

        public static Dictionary<int, List<ClientDay>> GetTest()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "//TestData");
            Dictionary<int, List<ClientDay>> data = new Dictionary<int, List<ClientDay>>();
            return GetData(directoryInfo.GetFiles("day*.json"));
        }

        public static Dictionary<int, List<ClientDay>> OpenFiles(string[] FileNames)
        {
            List<FileInfo> fileInfo = new List<FileInfo>();
            foreach (string fileName in FileNames)
            {
                fileInfo.Add(new FileInfo(fileName));
            }
            return GetData(fileInfo.ToArray());
        }

        public static void SaveUser(Client client, string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            string data = JsonConvert.SerializeObject(client);
            using (FileStream fstream = new FileStream($"{directoryInfo.FullName}.json", FileMode.OpenOrCreate))
            {
                byte[] array = Encoding.UTF8.GetBytes(data);
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}
