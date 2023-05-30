using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordingBook.Models
{
    public class CreateGreeting
    {

        //private string greeting { get; set; } = "Дорога {1} {2} {3}, від щирого серця вітаю тебе з днем народження. Бажаю щастя, достатку та найголовніше" +
        //    "здоров'я";
        private Record selectedRecord;
        public Record SelectedRecord
        {
            get { return selectedRecord; }
            set
            {
                selectedRecord = value;
            }
        }
        public static ObservableCollection<Record> whoHasBirthday { get; set; } = new ObservableCollection<Record>();
        static greetingWindow windowForGreet;

        public static void whoHasGotABirthday(ObservableCollection<Record> records)
        {
            windowForGreet = new greetingWindow();
            whoHasBirthday.Clear();
            foreach (Record record in records)
            {
                if(record.DateOfBirth == DateTime.Now.Date && record.formTheGreeting == false)
                {
                    whoHasBirthday.Add(record);
                }
            }

            windowForGreet.Show();
        }
    }
}
