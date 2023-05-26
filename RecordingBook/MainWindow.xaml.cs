﻿using System;
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



namespace RecordingBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Record> record = new List<Record>();
        Dictionary<string,string> CountryNames = null;

        public MainWindow()
        {
            DataContext = new Record();
            InitializeComponent();
            CountryNames = getCountryNames();
            List<string> CountryNames_list = CountryNames.Keys.ToList();
            CBCountry.ItemsSource = CountryNames_list;


            string AddButton_path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string DeleteButton_Path = AddButton_path + "\\images\\Delete_480px.png";
            AddButton_path += "\\images\\add_144px.png";


            var imageBrush_add = add_button.Background as ImageBrush;
            var imageBrush_delete = delete_button.Background as ImageBrush;

            imageBrush_add.ImageSource = new BitmapImage(new Uri(AddButton_path));
            imageBrush_delete.ImageSource = new BitmapImage(new Uri(DeleteButton_Path));
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            Record newElem = new Record()
            {
                FirstName = "FirstName",
                SecondName = "SecondName",
                LastName = "LastName",
                Age = "age",
                PhoneNumber = "+(number)"
            };

            record.Add(newElem);
            recordList.Items.Add(newElem);
            recordList.SelectedItem = newElem;
            add_button.IsEnabled = false;
        }

        private void recordList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fullForm.Children.Contains(TBNothingIs))
            {
                fullForm.Children.Remove(TBNothingIs);
            }
            setBlock.Visibility = Visibility.Visible;
            listBoxItem_Sinchronization();

            Record currentRecord = recordList.SelectedItem as Record;
            if (currentRecord != null)
            {
                gridData_Sinchronization(currentRecord);
            }
        }

        private void listBoxItem_Sinchronization()
        {
            Record CurrentRecord = recordList.SelectedItem as Record; ;
            secondName.Text = CurrentRecord.SecondName;
            firstName.Text = CurrentRecord.FirstName;
            lastName.Text = CurrentRecord.LastName;
            
        }

        private void gridData_Sinchronization(Record currentRecord)
        {
            currentRecord.FirstName = firstName.Text;
            currentRecord.LastName = lastName.Text;
            currentRecord.SecondName= secondName.Text;
        }

        private void NumberValidationTB(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void preventFromDeletePl(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Back && phoneNumber.Text.Length == 1 && phoneNumber.Text[0] == '+')
            {
                e.Handled=true;
            }
        }

        private Dictionary<string, string> getCountryNames()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            path += "\\csvTables\\Country-codes.csv";
            StreamReader reader = null;

            Dictionary<string,string> countries = new Dictionary<string, string>();
            if (File.Exists(path))
            {
                reader = new StreamReader(File.OpenRead(path));

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    countries.Add(values[1], values[2]);
                }
                return countries;
            }
            else
            {
                string messageBoxText = "There is a problem";
                string caption = "Word Processor";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                return null;
            }

        }

        private void applyClick(object sender, RoutedEventArgs e)
        {
            Record CurrentRecord = recordList.SelectedItem as Record;

            if (CurrentRecord != null)
            {
                CurrentRecord.FirstName = firstName.Text;
                CurrentRecord.LastName = lastName.Text;
                CurrentRecord.SecondName = secondName.Text;
                CurrentRecord.Age = HowOld().ToString();
                recordList.Items.Refresh();
                add_button.IsEnabled = true;
            }

        }
        private int HowOld()
        {
            int years = 0;
            if(dateBirthCa.SelectedDate.HasValue)
            {
                DateTime dateOfBirth = dateBirthCa.SelectedDate.Value;
                DateTime currentDate = DateTime.Now;
                years= currentDate.Year - dateOfBirth.Year;
                if (dateOfBirth.Date > currentDate.AddYears(-years)) years--;
            }
            return years;
        }

        private void addButton_enter(object sender, MouseEventArgs e)
        {
            string AddButton_path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            AddButton_path += "\\images\\add_144px_enter_.png";

            var imageBrush_add = add_button.Background as ImageBrush;
            imageBrush_add.ImageSource = new BitmapImage(new Uri(AddButton_path));
        }

        private void addButton_leave(object sender, MouseEventArgs e)
        {
            string AddButton_path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            AddButton_path += "\\images\\add_144px.png";

            var imageBrush_add = add_button.Background as ImageBrush;
            imageBrush_add.ImageSource = new BitmapImage(new Uri(AddButton_path));
        }

        private void deleteButton_enter(object sender, MouseEventArgs e)
        {
            string DeleteButton_path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            DeleteButton_path += "\\images\\Delete_480px_enter.png";

            var imageBrush_delete = delete_button.Background as ImageBrush;
            imageBrush_delete.ImageSource = new BitmapImage(new Uri(DeleteButton_path));
        }

        private void deleteButton_leave(object sender, MouseEventArgs e)
        {
            string DeleteButton_path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            DeleteButton_path += "\\images\\Delete_480px.png";

            var imageBrush_delete = delete_button.Background as ImageBrush;
            imageBrush_delete.ImageSource = new BitmapImage(new Uri(DeleteButton_path));
        }

        private void combBox_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = CBCountry.SelectedItem;
            if (selectedItem != null)
            {
                string selectedValue = selectedItem.ToString();
                phoneNumber.Text = phoneNumber.Text.ToString()[0] + CountryNames[selectedValue];
            }
        }

        private void searchButton_click(object sender, RoutedEventArgs e)
        {
            int countPosition = 0;
            if (searchField.Text[0] != '@')
            {
                for(int i = recordList.Items.Count - 1; i >=0; i--)
                {
                    Record currRec =(Record) recordList.Items.GetItemAt(i);
                    if(currRec.FirstName == searchField.Text)
                    {
                        recordList.Items.RemoveAt(i);
                        recordList.Items.Insert(0, currRec);
                        countPosition++;
                    }
                }
                for(int i = countPosition-1; i < recordList.Items.Count - 1; i++)
                {
                    ListBoxItem item = (ListBoxItem)recordList.ItemContainerGenerator.ContainerFromIndex(i);
                    item.Visibility= Visibility.Hidden;
                }
            }
        }
        private void ValidateTextBox (object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if(textBox != null && !string.IsNullOrWhiteSpace(textBox.Text)) 
            {
                textBox.BorderBrush = new SolidColorBrush(System.Windows.Media.Colors.Red);
            }
        }

        private void firstName_textChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();


        }

        private void firstName_lostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            BindingExpression bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpression.UpdateSource();

            if (!bindingExpression.HasError)
            {
                string propertyName = bindingExpression.ParentBinding.Path.Path;
                ((Record)DataContext).ClearErrors(propertyName);
            }
        }
    }
}

