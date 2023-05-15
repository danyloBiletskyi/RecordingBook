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



namespace RecordingBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Record> record = new List<Record>();
        string[] CountryNames = null;

        public MainWindow()
        {
            InitializeComponent();
            CountryNames = getCountryNames();
            CBCountry.ItemsSource = CountryNames;
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            record.Add(new Record() { FirstName = "FirstName", SecondName = "SecondName", LastName = "LastName", Age = "age", PhoneNumber = "+(number)" });
            recordList.Items.Add(record);
            recordList.SelectedItem = record;
            add_button.IsEnabled= false;
        }

        private void recordList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setBlock.Children.Clear();
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

        private string[] getCountryNames()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            path += "\\csvTables\\Country-codes.csv";
            StreamReader reader = null;

            List<string> countries = new List<string>();
            if (File.Exists(path))
            {
                reader = new StreamReader(File.OpenRead(path));

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    countries.Add(values[1]);
                }
                return countries.ToArray();
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
    }
}
