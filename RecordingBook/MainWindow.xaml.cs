using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RecordingBook.Models;
using System.Collections.ObjectModel;
using RecordingBook.Additional_ViewAndComfort;
using System.Windows.Controls.Primitives;

namespace RecordingBook
{
    // Це головний клас в якому відбуватимуться виклики та ініціалізація дій, зроблених користувачем.
    public partial class MainWindow : Window
    {
        private bool isComboBoxChanged = false;
        private Dictionary<string, string>? countryNames = default!;



        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;


            Records = RecordManager.GetRecord();

            SaveLoadInitialise.Load(Records);
            SortingComboBox.SelectedIndex = 0;

            countryNames = DBCountryNames_Initialiser.GetCountryData();
            if (countryNames == null)
            {
                return;
            }
            List<string> CountryNames_list = countryNames.Keys.ToList();
            CBCountry.ItemsSource = CountryNames_list;

        }

        public DateTime DateNow //Властивість, що передаватиметься у форму при створенні запису
                                //задля встановлення кінцевої дати дня народження.
        {
            get { return DateTime.Now; }
            set { value = DateTime.Now; }
        }

        public ObservableCollection<Record> Records { get; set; }
        private Record CurrentRecord { get; set; } = default!;


        // Метод, для виклику пошуку при натисканні на кнопку пошуку.
        private void SearchButton_click(object sender, RoutedEventArgs e)
        {
            RecordSearch.SearchRecord(RecordList, SearchField.Text);
        }


        // Виклик оновлення даних, якщо змінилося значення поля пошуку.
        private void SearchField_Changed(object sender, TextChangedEventArgs e)
        {
            RecordSearch.AutoRefresh(RecordList, SearchField.Text);
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e, int targetIndex)
        {
            if (RecordList.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                ListBoxItem listBoxItem = (ListBoxItem)RecordList.ItemContainerGenerator.ContainerFromIndex(targetIndex);
                if (listBoxItem != null)
                {
                    // Виконайте потрібні дії з listBoxItem
                    // ...
                }
            }
        }
        // Метод для створення запису.
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentRecord != null)
            {
                if (CurrentRecord.HasErrors)
                {
                    return;
                }
            }

            Record newElem = new Record();
            Records.Add(newElem);
            RecordList.SelectedIndex = RecordList.Items.Count - 1;

        }


        // Ініціалізація збереження даних, при натисканні на кнопку.
        private void SaveAllButton_Click(object sender, RoutedEventArgs e)
        {
            SaveLoadInitialise.Save(Records);
        }


        // Метод видалення запису при натисканні на кнопку.
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Records.Count > 0)
            {
                Records.Remove(CurrentRecord);
                RecordList.SelectedIndex = 0;
                if (Records.Count > 1)
                {
                    CurrentRecord = Records[0];
                    ViewControl.CheckForErrorsAndMakeSelectionable(CurrentRecord,
                    RecordList);
                }
            }
            if(Records.Count == 0)
            {
                ViewControl.MakeHidden(SetBlock, TBNothingIs);
            }

        }


        // Метод, що виконує функцію формування привітання, якщо натиснута
        // відповідна кнопка.
        private void BirthdayGet_Click(object sender, RoutedEventArgs e)
        {
            CreateGreeting.whoHasABirthday(Records);
        }


        // Метод, що викликає певне сортування,
        // якщо значення у полі вибору змінилося.
        private void SortCB_SelectionChanged(object sender,
            SelectionChangedEventArgs e)
        {
            ComboBoxItem? selItem = SortingComboBox.SelectedItem as ComboBoxItem;

            if (selItem == null)
            {
                return;
            }
            string? selItemStr = selItem.Content.ToString();
            var sRecords = RecordsSort.GetSortedRecord(selItemStr, Records);
            Records.Clear();

            foreach (var sortedRecord in sRecords)
            {
                Records.Add(sortedRecord);
            }
            RecordList.Items.Refresh();
            RecordList.SelectedIndex = 0;
        }


        // Метод, що викликає перевірку та відповідні зміни,
        // якщо є помилки в записі.
        private void RecordList_Loaded(object sender, RoutedEventArgs e)
        {
            ViewControl.CheckForErrorsAndMakeSaveable(CurrentRecord,
                SaveAllButton);
            ViewControl.CheckForErrorsAndMakeSelectionable(CurrentRecord,
                RecordList);
        }


        // Метод, що виконує певні дії при виборі користувачем якогось запису.
        private void RecordList_SelectionChanged(object sender,
            SelectionChangedEventArgs e)
        {
            ListBox? Lb = sender as ListBox;

            if (Lb == null || Lb.SelectedItem == null)
            {
                return;
            }

            CurrentRecord = Lb.SelectedItem as Record;
            if (CurrentRecord != null)
            {
                ViewControl.MakeVisible(SetBlock, TBNothingIs);
                isComboBoxChanged = false;
            }

        }


        // Метод, що викликає перевірку значень ПІБ, якщо вони змінилися.
        private void FSL_changed(object sender, TextChangedEventArgs e)
        {
            RecordList_Loaded(RecordList, e);
        }


        // Метод, для виділення тексту при натисканні на відповідне поле.
        private void SelectAllText(object sender, MouseButtonEventArgs e)
        {
            TextBox? tb = sender as TextBox;

            if (tb == null)
            {
                return;
            }
            tb.Dispatcher.BeginInvoke(new Action(() =>      // Даний метод викликає подію у асинхронному режимі. Це через те, що при натисканні наш
            //tb знаходиться у проміжному стані, що перешкоджає виконанню виділення всього тексту
            {
                if (tb.Name == phoneNumber.Name)
                {
                    tb.Focus();
                    tb.SelectionStart = 1;
                    tb.SelectionLength = tb.Text.Length - 1;
                }
                else
                {
                    tb.SelectAll();
                }

            }));
        }


        // Метод для ініціалізації реакції поля для номеру,
        // на дії з полем вибору країни
        private void CombBox_SelectionChanged(object sender, 
            SelectionChangedEventArgs e)
        {
            if (isComboBoxChanged)
            {

                string selectedItem = (string) CBCountry.SelectedItem;
                if (selectedItem != null)
                {
                    if (countryNames == null)
                    {
                        return;
                    }
                    phoneNumber.Text = phoneNumber.Text.ToString()[0] + 
                        countryNames[selectedItem];
                }
            }
        }



        // Переривання не бажаних дій користувача, щодо поля номеру
        private void PNumberRestrict_PreviewKeyDown(object sender, 
            KeyEventArgs e)
        {
            if (e.Key == Key.Back && phoneNumber.Text.Length == 1 && 
                phoneNumber.Text[0] == '+')
            {
                e.Handled = true;
            }
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & 
                e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Left && phoneNumber.SelectionStart > 1)
            {
                phoneNumber.SelectionStart-= 1;
            }
            else if (e.Key == Key.Right && phoneNumber.Text.Length > 
                phoneNumber.SelectionStart)
            {
                phoneNumber.SelectionStart += 1;
            }
        }


        // Метод виклику перевірки введених користувачем даних
        private void phoneNumber_TextUpdated(object sender, 
            TextChangedEventArgs e)
        {
            ViewControl.PhoneNumberTBRule(phoneNumber);
        }



        // Метод, що перевіряє чи введені дані в полі для вибору дати коректні.
        // Викликає обчислення віку, якщо дані вірні
        private void datePicker_ClosedCheck(object sender, RoutedEventArgs e)
        {
            DatePicker? dP = sender as DatePicker;

            if (dP == null)
            {
                return;
            }
            if (dP.SelectedDate.HasValue && 
                dP.SelectedDate.Value != DateTime.MinValue)
            {
                CurrentRecord.DateOfBirth = dP.SelectedDate.Value;
                CurrentRecord.Age = Record.GetAge(dP.SelectedDate).ToString();
            }
            else
            {
                dP.SelectedDate = DateTime.Now;
            }
            RecordList.Items.Refresh();
        }




        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            isComboBoxChanged = true;
        }


        private void ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            isComboBoxChanged = false;
        }


        // Ініціалізація збереження даних, при закритті вікна.
        private void Window_Closing(object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if(CurrentRecord!= null)
            {
                if (CurrentRecord.HasErrors)
                {
                    Records.Remove(CurrentRecord);
                }
            }

            SaveLoadInitialise.Save(Records);
        }



    }
}

