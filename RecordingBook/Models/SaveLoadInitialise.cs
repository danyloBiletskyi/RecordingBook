using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Text.Json;
using Newtonsoft.Json;

namespace RecordingBook.Models
{
    public class SaveLoadInitialise //Це клас створений для виконання
                                    //таких завдань як Збереження та Завантаження даних.
    {
        static string pathSL;
        public static void Save(ObservableCollection<Record> records)
        {
            GetSLFilePath();
            StringBuilder jsonToSave = new StringBuilder();

            for (int i = 0; i < records.Count; i++)
            {
                var jsonString = System.Text.Json.JsonSerializer.Serialize(records[i]);

                jsonToSave.Append(jsonString);
            }
            File.WriteAllText(pathSL, jsonToSave.ToString());
        }

        public static void Load(ObservableCollection<Record> records)
        {
            GetSLFilePath();
            string fileContent = File.ReadAllText(pathSL);
            using (var stringReader = new StringReader(fileContent))
            {
                var jsReader = new JsonTextReader(stringReader)
                {
                    SupportMultipleContent = true
                };

                var jsonSerialiser = new Newtonsoft.Json.JsonSerializer();
                while (jsReader.Read())
                {
                    Record? rec = jsonSerialiser.Deserialize<Record>(jsReader);
                    records.Add(rec);
                }
            }
        }


        private static void GetSLFilePath()
        {
            string currDir = Directory.GetCurrentDirectory();

            if (currDir != null)
            {
                DirectoryInfo? parrDir = Directory.GetParent(currDir);
                if (parrDir != null)
                {
                    string? saveDir = parrDir.Parent?.Parent?.FullName;
                    if (saveDir != null)
                    {
                        string path =  System.IO.Path.Combine(saveDir, "Save", 
                            "Save.json");
                        if (!File.Exists(path))
                        {
                            File.Create(path);
                        }
                        pathSL = path;
                    }
                }
            }
            return;
        }

    }

}
