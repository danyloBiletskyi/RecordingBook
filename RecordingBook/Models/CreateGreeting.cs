using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordingBook.Models
{
    public class CreateGreeting // Клас, призначений для реалізації методів
                                //формування привітання.
    {

        static greetingWindow? windowForGreet;

        public Record? SelectedRecord { get; set; }
        public static ObservableCollection<Record> WhoHasBirthday { get; set; } =
            new ObservableCollection<Record>();


        // Метод що створює вікно для відображення дня народження та зображає
        // там іменинників, якщо вони там є.
        public static void whoHasABirthday(ObservableCollection<Record> records)
        {
            windowForGreet = new greetingWindow();
            WhoHasBirthday.Clear();
            foreach (Record record in records)
            {
                if (record.DateOfBirth.Month == DateTime.Now.Month &&
                    record.DateOfBirth.Day== DateTime.Now.Day &&
                    record.FormTheGreeting == false)
                {
                    WhoHasBirthday.Add(record);
                }
            }

            windowForGreet.Show();
        }
    }
}
