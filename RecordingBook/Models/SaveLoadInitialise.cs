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

namespace RecordingBook.Models
{
    public class SaveLoadInitialise
    {
        static string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Save\\Save.csv";
        static FileInfo file = new FileInfo(path);
        public static void SaveOne(Record record)
        {
            Check_CreatePath();
            RewriteComma(record);
            SaveOne_Process(record);

        }

        public static void SaveAll(ObservableCollection<Record> records)
        {
            Check_CreatePath();
            RewriteCommas(records);
            WriteIntoCSV(records);
        }


        private static void Check_CreatePath()
        {
            if(!File.Exists(path))
            {
                File.Create(path);
            }
        }


        private static void RewriteCommas(ObservableCollection<Record> records)
        {
            foreach (Record record in records)
            {
                RewriteComma(record);
            }
        }

        private static void RewriteComma(Record record)
        {
            StringBuilder City = new StringBuilder(record.City).Replace(",", "*");
            record.City = City.ToString();
            StringBuilder StreenNumber = new StringBuilder(record.StreetAndNumber).Replace(",", "*");
            record.StreetAndNumber = StreenNumber.ToString();
            StringBuilder PlaceOfStudyWork = new StringBuilder(record.PlaceOfWorkStudy).Replace(",", "*");
            record.PlaceOfWorkStudy = PlaceOfStudyWork.ToString();
            StringBuilder AdditionalInfo = new StringBuilder(record.AdditionalInfo).Replace(",", "*");
            record.AdditionalInfo = AdditionalInfo.ToString();
        }

        private static void RewriteCommasBack(ObservableCollection<Record> records)
        {
            foreach (Record record in records)
            {
                RewriteCommaBack(record);
            }
        }

        private static void RewriteCommaBack(Record record)
        {
            StringBuilder City = new StringBuilder(record.City).Replace("*", ",");
            record.City = City.ToString();
            StringBuilder StreenNumber = new StringBuilder(record.StreetAndNumber).Replace("*", ",");
            record.StreetAndNumber = StreenNumber.ToString();
            StringBuilder PlaceOfStudyWork = new StringBuilder(record.PlaceOfWorkStudy).Replace("*", ",");
            record.PlaceOfWorkStudy = PlaceOfStudyWork.ToString();
            StringBuilder AdditionalInfo = new StringBuilder(record.AdditionalInfo).Replace("*", ",");
            record.AdditionalInfo = AdditionalInfo.ToString();
        }

        private static void WriteIntoCSV(ObservableCollection<Record> records)
        {
            var csv = new StringBuilder();
            foreach (Record record in records)
            {
                var toWrite = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", record.SecondName, record.FirstName, record.LastName,
                    record.Age, record.StreetAndNumber, record.Country, record.City, record.PhoneNumber, record.PlaceOfWorkStudy, record.AdditionalInfo,
                    record.DateOfBirth.ToShortDateString(), record.dateOfCreation.ToString(), record.recordID.ToString()); 
                csv.AppendLine(toWrite);
            }

            File.WriteAllText(path, csv.ToString());

        }


        public static void Load(ObservableCollection<Record> Records)
        {
            StreamReader reader = null;
            List<string> recordInfo = new List<string>();
            if(File.Exists(path))
            {
                reader = new StreamReader(File.OpenRead(path));

                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    recordInfo = line.Split(',').ToList();
                    Records.Add(new Record() { SecondName = recordInfo[0], FirstName = recordInfo[1], LastName = recordInfo[2], Age = recordInfo[3],
                        StreetAndNumber = recordInfo[4], Country = recordInfo[5], City = recordInfo[6], PhoneNumber = recordInfo[7], PlaceOfWorkStudy = recordInfo[8],
                        AdditionalInfo = recordInfo[9], DateOfBirth = DateTime.Parse(recordInfo[10]), dateOfCreation = DateTime.Parse(recordInfo[11]), recordID = int.Parse(recordInfo[12])});
                }
                RewriteCommasBack(Records);
                reader.Close();
            }
        }

        private static void SaveOne_Process(Record record)
        {
            List<string> recordInfo = new List<string>();
            StreamReader reader = new StreamReader(File.OpenRead(path));
            if(file.Length > 0)
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    recordInfo = line.Split(',').ToList();
                    if (record.recordID.ToString() != recordInfo[12])
                    {
                        line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", record.SecondName, record.FirstName, record.LastName,
                            record.Age, record.StreetAndNumber, record.Country, record.City, record.PhoneNumber, record.PlaceOfWorkStudy, record.AdditionalInfo,
                            record.DateOfBirth.ToShortDateString(), record.dateOfCreation.ToString(), record.recordID.ToString());
                    }
                    recordInfo.Add(line);
                }
            }
            else
            {
                string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", record.SecondName, record.FirstName, record.LastName,
                            record.Age, record.StreetAndNumber, record.Country, record.City, record.PhoneNumber, record.PlaceOfWorkStudy, record.AdditionalInfo,
                            record.DateOfBirth.ToShortDateString(), record.dateOfCreation.ToString(), record.recordID.ToString());
                recordInfo.Add(line);
            }
            reader.Close();
            File.WriteAllLines(path, recordInfo);
        }
    }

}
