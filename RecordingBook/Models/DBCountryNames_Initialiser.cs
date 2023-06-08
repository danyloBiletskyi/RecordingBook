using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordingBook.Models
{
    public class DBCountryNames_Initialiser
    {
        static string path = GetPath();

        // Метод для обробки та отримання даних, щодо країн
        // та їх номерних кодів з .csv таблиці
        public static Dictionary<string, string>? GetCountryData()
        {
            StreamReader reader = default!;
            Dictionary<string, string>? countries = new Dictionary<string,
                string>();

            if (File.Exists(path))
            {
                reader = new StreamReader(File.OpenRead(path));

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                    {
                        return null;
                    }
                    var values = line.Split(',');
                    countries.Add(values[1], values[2]);
                }
                return countries;
            }
            else
            {
                return null;
            }
        }


        private static string GetPath()
        {
            string currDir = Directory.GetCurrentDirectory();

            if (currDir != null)
            {
                DirectoryInfo? parDir = Directory.GetParent(currDir);
                if (parDir != null)
                {
                    string? tableDir = parDir.Parent?.Parent?.FullName;
                    if (tableDir != null)
                    {
                        return Path.Combine(tableDir, "csvTables",
                            "Country-codes.csv");
                    }
                }
            }
            return string.Empty;
        }
    }
}
