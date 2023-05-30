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
        static string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;


        public static Dictionary<string, string> GetCountryData()
        {
            path += "\\csvTables\\Country-codes.csv";
            StreamReader reader = null;

            Dictionary<string, string> countries = new Dictionary<string, string>();
            if (File.Exists(path))
            {
                reader = new StreamReader(File.OpenRead(path));

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
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
    }
}
