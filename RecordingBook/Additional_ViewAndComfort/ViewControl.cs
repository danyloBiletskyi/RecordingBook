using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RecordingBook;
using System.Windows.Input;

namespace RecordingBook.Additional_ViewAndComfort
{
    public static class ViewControl
    {

        public static void PhoneNumberTBRule(object TBObject)
        {
            TextBox TB = TBObject as TextBox;
            if(TB.Text.Length == 0)
            {
                TB.Text = "+";
                TB.SelectionStart = 1;
            }
        }
    }
}
