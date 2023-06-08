using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordingBook.Models
{
    public class RecordManager
    {
        public static ObservableCollection<Record> RecordList { get; set; } =
            new ObservableCollection<Record>();

        public static ObservableCollection<Record> GetRecord()
        {
            return RecordList;
        }


    }
}
