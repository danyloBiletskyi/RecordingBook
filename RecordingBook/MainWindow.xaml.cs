using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.IO;
using CsvHelper;
using System.Globalization;
using RecordingBook.Models;
using System.Collections.ObjectModel;
using RecordingBook.Additional_ViewAndComfort;
using System.Runtime.Intrinsics.Arm;
using System.Reflection;

namespace RecordingBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isComboBoxChanged = false;
        public DateTime DateNow { get; set; }
        public Record CurrentRecord { get; set; }
        public ObservableCollection<Record> Records { get; set; }
        Dictionary<string,string> CountryNames = null;

        public MainWindow()
        {
            InitializeComponent();
            Records = RecordManager.GetRecord();

            SaveLoadInitialise.Load(Records);
            sortBy.SelectedIndex = 0;
            DataContext = this;
            CountryNames = DBCountryNames_Initialiser.GetCountryData();
            List<string> CountryNames_list = CountryNames.Keys.ToList();
            CBCountry.ItemsSource = CountryNames_list;

        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            Record newElem = new Record();
            Records.Add(newElem);

            recordList.SelectedIndex = recordList.Items.Count - 1;
        }


        private void combBox_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_isComboBoxChanged) { 
                var selectedItem = CBCountry.SelectedItem;
                if (selectedItem != null)
                {
                    string selectedValue = selectedItem.ToString();
                    phoneNumber.Text = phoneNumber.Text.ToString()[0] + CountryNames[selectedValue];
                }
            }
        }

        private void searchButton_click(object sender, RoutedEventArgs e)
        {
            RecordSearch.SearchFunc(recordList, searchField.Text);
        }




        private void PNumberRestrict_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back && phoneNumber.Text.Length == 1 && phoneNumber.Text[0] == '+')
            {
                e.Handled = true;
            }
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void phoneNumber_TextUpdated(object sender, TextChangedEventArgs e)
        {
            ViewControl.PhoneNumberTBRule(phoneNumber);
        }


        private void recordList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox Lb = sender as ListBox;
            CurrentRecord = Lb.SelectedItem as Record;
            ViewControl.MakeVisible(setBlock, TBNothingIs);
            _isComboBoxChanged = false;
        }

        private void searchField_Changed(object sender, TextChangedEventArgs e)
        {
            RecordSearch.AutoRefreshing(recordList, searchField.Text);
        }

        private void datePicker_ClosedCheck(object sender, RoutedEventArgs e)
        {
            DatePicker DP = sender as DatePicker;
            if (DP.SelectedDate.HasValue && DP.SelectedDate.Value != DateTime.MinValue)
            {
                CurrentRecord.DateOfBirth = DP.SelectedDate.Value;
                CurrentRecord.Age = Record.GetAge(DP.SelectedDate).ToString();
            }
            else
            {
                DP.SelectedDate = DateTime.Now;
            }
            recordList.Items.Refresh();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveLoadInitialise.SaveOne(CurrentRecord);
        }

        private void saveAllButton_Click(object sender, RoutedEventArgs e)
        {
            SaveLoadInitialise.SaveAll(Records);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveLoadInitialise.SaveAll(Records);
        }

        private void comboBox_gorFocus(object sender, RoutedEventArgs e)
        {
            _isComboBoxChanged = true;
        }

        private void comboBox_lostFocus(object sender, RoutedEventArgs e)
        {
            _isComboBoxChanged = false;
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            Records.Remove(CurrentRecord);
        }

        private void sortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = sortBy.SelectedItem as ComboBoxItem;
            var sortedRecords = RecordsSort.GetSortedRecord(selectedItem.Content.ToString(), Records);
            Records.Clear();

            foreach(var sortedRecord in sortedRecords)
            {
                Records.Add(sortedRecord);
            }
            recordList.Items.Refresh();
            recordList.SelectedIndex= 0;
        }

        private void birthdayGet_Click(object sender, RoutedEventArgs e)
        {
            CreateGreeting.whoHasGotABirthday(Records);
        }
    }
}

