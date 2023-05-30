using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RecordingBook.Models
{
    public static class RecordSearch
    {
        public static void SearchFunc(ListBox listBox, string searchText)
        {
            if (searchText == null) { return; }
            else
            {
                searchText = searchText.ToLower();
                Dictionary<string, string> SearchParameters = ParameterGetting(searchText);
                if (SearchParameters == null) return;
                bool containsSpecialCheck = searchText.Contains('@');
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    ListBoxItem lbi = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(i);
                    Record record_ofLBI = listBox.Items.GetItemAt(i) as Record;
                    if (containsSpecialCheck)
                    {
                        if (DataChecking(SearchParameters, record_ofLBI))
                        {
                            lbi.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            lbi.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                    else if (!containsSpecialCheck)
                    {
                        if (record_ofLBI.FirstName.ToLower().Contains(searchText) || record_ofLBI.SecondName.ToLower().Contains(searchText) || record_ofLBI.LastName.ToLower().Contains(searchText))
                        {
                            lbi.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            lbi.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }

                }
            }

        }
        private static Dictionary<string, string> ParameterGetting(string searchText)
        {
            Dictionary<string, string> finalParameters = new Dictionary<string, string>();
            int countOfParameters = searchText.Count(s => s == '@');
            StringBuilder newST = new StringBuilder(searchText);
            newST.Replace(" ", "");

            List<string> ST_searchParams = newST.ToString().Split('@').ToList();
            ST_searchParams.RemoveAt(0);
            for(int i = 0; i < ST_searchParams.Count; i++)
            {
                string[] currentValueKey = ST_searchParams[i].Split(':');
                finalParameters.Add(currentValueKey[0].ToLower(), currentValueKey[1].ToLower());
            }
            if (!ParameterChecking(finalParameters))
            {
                return null;
            }
            return finalParameters;
        }

        private static bool ParameterChecking(Dictionary<string, string> finalParameters)
        {
            int CountOfKeyValues = finalParameters.Count;
            string[] correctKeys = {"ім'я", "ім`я", "прізвище", "побатькові", "вік", "країна", "місто" };
            foreach (string key in correctKeys)
            {
                if (finalParameters.ContainsKey(key))
                {
                    CountOfKeyValues--;
                }
            }
            if (CountOfKeyValues == 0) return true; else return false;
        }

        private static bool DataChecking(Dictionary<string, string> finalParameters, Record currentRecord)
        {
            int isExist = finalParameters.Count;
            foreach(string key in finalParameters.Keys)
            {
                switch (key)
                {
                    case "ім'я":
                        if(currentRecord.FirstName.ToLower() == finalParameters[key].ToLower())
                        {
                            isExist--;
                        }
                        break;
                    case "ім`я":
                        if (currentRecord.FirstName.ToLower() == finalParameters[key].ToLower())
                        {
                            isExist--;
                        }
                        break;
                    case "прізвище":
                        if (currentRecord.SecondName.ToLower() == finalParameters[key].ToLower())
                        {
                            isExist--;
                        }
                        break;
                    case "побатькові":
                        if (currentRecord.LastName.ToLower() == finalParameters[key].ToLower())
                        {
                            isExist--;
                        }
                        break;
                    case "вік":
                        if (currentRecord.Age.ToLower() == finalParameters[key].ToLower())
                        {
                            isExist--;
                        }
                        break;
                    case "країна":
                        if (currentRecord.Country.ToLower() == finalParameters[key].ToLower())
                        {
                            isExist--;
                        }
                        break;
                    case "місто":
                        if (currentRecord.City.ToLower() == finalParameters[key].ToLower())
                        {
                            isExist--;
                        }
                        break;
                }
            }
            if(isExist == 0)
            {
                return true;
            }
            return false;
        }


        public static void AutoRefreshing (ListBox listBox, string searchText)
        {
            if(searchText == "")
            {
                for(int i = 0; i < listBox.Items.Count; i++)
                {
                    ListBoxItem lb =(ListBoxItem) listBox.ItemContainerGenerator.ContainerFromIndex(i);
                    lb.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
    }
}
