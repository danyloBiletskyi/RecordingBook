using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RecordingBook;
using System.Windows.Input;
using RecordingBook.Models;

namespace RecordingBook.Additional_ViewAndComfort
{
    public static class ViewControl //Клас в якому реалізуються методи, що
                                    //використовуються виключно для налаштувань
                                    //візуальної частини застосунку.
    {


        // Перевірка на наявність правильно введеного номеру телефону.
        public static void PhoneNumberTBRule(object tbObject)   

        {
            TextBox? textBox = tbObject as TextBox;
            if (textBox == null)
            {
                return;
            }
            if (textBox.Text.Length == 0)
            {
                textBox.Text = "+";
                textBox.SelectionStart = 1;
            }
            if (textBox.Text[0] != '+')
            {
                textBox.Text = "+";
            }
        }


        // Метод котрий при відсутності обраного запису
        // змінює видимість блоків інтерфейсу.
        public static void MakeHidden(Grid setBlock, TextBlock tbNothingIs)
        {
            setBlock.Visibility = System.Windows.Visibility.Hidden;
            tbNothingIs.Visibility = System.Windows.Visibility.Visible;
        }

        public static void MakeVisible(Grid setBlock, TextBlock tbnothing_is)
        {
            setBlock.Visibility = System.Windows.Visibility.Visible;
            tbnothing_is.Visibility = System.Windows.Visibility.Hidden;
        }


        // Метод що встановлює збереження даних доступним
        // в залежності від наявності помилок у поточному записі.
        public static void CheckForErrorsAndMakeSaveable(
            Record currentRecord, Button Button)
        {
            if (currentRecord == null)
            {
                return;
            }
            if (currentRecord != null && currentRecord.HasErrors)
            {
                Button.IsEnabled= false;
            }
            else
            {
                Button.IsEnabled= true;
            }
        }


        // Метод що встановлює недоптупними для вибору інші записи,
        // якщо поточний запис має помилку.
        public static void CheckForErrorsAndMakeSelectionable(
            Record currentRecord, ListBox lb)
        {
            if (currentRecord == null)
            {
                return;
            }
            if (currentRecord.HasErrors)
            {
                foreach (var listItem in lb.Items)
                {
                    var listBoxItem = new ListBoxItem();
                    listBoxItem = (ListBoxItem)lb.ItemContainerGenerator.ContainerFromItem(listItem);
                    listBoxItem.IsHitTestVisible = (listItem == currentRecord);
                }
            }
            else
            {
                foreach (var listItem in lb.Items)
                {
                    var listBoxItem = new ListBoxItem();
                    listBoxItem = (ListBoxItem)lb.ItemContainerGenerator.ContainerFromItem(listItem);
                    if (listBoxItem == null)
                    {
                        continue;
                    }
                    listBoxItem.IsHitTestVisible = true;
                }
            }

        }
    }
}
