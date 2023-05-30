using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordingBook.Models
{
    public class RecordsSort
    {

        public static ObservableCollection<Record> GetSortedRecord(string answer, ObservableCollection<Record> records)
        {
            ObservableCollection<Record> recordsToReturn= new ObservableCollection<Record>();
            if(answer != null)
            {
                if(answer== "Ім'я від А-Я")
                {
                    recordsToReturn = SortByNameUpDown(records);
                }
                else if (answer == "Ім'я від Я-А")
                {
                    recordsToReturn = SortByNameDownUp(records);
                }
                else if (answer == "Спочатку нові")
                {
                    recordsToReturn = SortByDateNewFirst(records);
                }
                else
                {
                    recordsToReturn = SortByDateNewLast(records);
                }
            }
            return recordsToReturn;
        }

        private static ObservableCollection<Record> SortByNameUpDown(ObservableCollection<Record> records)
        {
            ObservableCollection<Record> sortedRecords = new ObservableCollection<Record>(records.OrderBy(record => record.SecondName));
            if(sortedRecords != null)
            {
                records = sortedRecords;
            }
            return records;
        }

        private static ObservableCollection<Record> SortByNameDownUp(ObservableCollection<Record> records)
        {
            ObservableCollection<Record> sortedRecords = new ObservableCollection<Record>(records.OrderByDescending(record => record.SecondName));
            if (sortedRecords != null)
            {
                records = sortedRecords;
            }
            return records;
        }

        private static ObservableCollection<Record> SortByDateNewLast(ObservableCollection<Record> records)
        {
            ObservableCollection<Record> sortedRecords = new ObservableCollection<Record>(records.OrderBy(record => record.dateOfCreation));
            if (sortedRecords != null)
            {
                records = sortedRecords;
            }
            return records;
        }

        private static ObservableCollection <Record> SortByDateNewFirst(ObservableCollection<Record> records)
        {
            ObservableCollection<Record> sortedRecords = new ObservableCollection<Record>(records.OrderByDescending(record => record.dateOfCreation));
            if (sortedRecords != null)
            {
                records = sortedRecords;
            }
            return records;
        }
    }
}
