using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordingBook.Models
{
    public class RecordsSort //Клас, створений для реалізації методів
                             //сортування в різних порядках.
    {


        // Основний метод, що повертає відсортовану колекцію записів.
        public static ObservableCollection<Record> GetSortedRecord(string text,
            ObservableCollection<Record> records)
        {
            ObservableCollection<Record> recsToRet= new ObservableCollection<Record>();

            if (text != null)
            {
                if (text== "Ім'я від А-Я")
                {
                    recsToRet = SortByNameUpDown(records);
                }
                else if (text == "Ім'я від Я-А")
                {
                    recsToRet = SortByNameDownUp(records);
                }
                else if (text == "Спочатку нові")
                {
                    recsToRet = SortByDateNewFirst(records);
                }
                else
                {
                    recsToRet = SortByDateNewLast(records);
                }
            }
            return recsToRet;
        }


        //Метод, що сортує записи за алфавітним порядком прізвища.
        private static ObservableCollection<Record> SortByNameUpDown(
            ObservableCollection<Record> records)
        {
            ObservableCollection<Record> sortedRecords = new 
                ObservableCollection<Record>(records.OrderBy
                (record => record.SecondName));

            if (sortedRecords != null)
            {
                records = sortedRecords;
            }
            return records;
        }


        //Метод, що сортує записи у зворотньому алфавітному порядку прізвища.
        private static ObservableCollection<Record> SortByNameDownUp(
            ObservableCollection<Record> records)
        {
            ObservableCollection<Record> sortedRecords = new 
                ObservableCollection<Record>(records.OrderByDescending
                (record => record.SecondName));

            if (sortedRecords != null)
            {
                records = sortedRecords;
            }
            return records;
        }


        //Метод, що сортує записи у такому порядку, що першими йдуть нові.
        private static ObservableCollection<Record> SortByDateNewLast(
            ObservableCollection<Record> records)
        {
            ObservableCollection<Record> sortedRecords = new
                ObservableCollection<Record>(records.OrderBy
                (record => record.DateOfCreation));

            if (sortedRecords != null)
            {
                records = sortedRecords;
            }
            return records;
        }


        //Метод, що сортує записи у такому порядку, що першими йдуть старі.
        private static ObservableCollection <Record> SortByDateNewFirst(
            ObservableCollection<Record> records)
        {
            ObservableCollection<Record> sortedRecords = new
                ObservableCollection<Record>(records.OrderByDescending
                (record => record.DateOfCreation));

            if (sortedRecords != null)
            {
                records = sortedRecords;
            }
            return records;
        }
    }
}
