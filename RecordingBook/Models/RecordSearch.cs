﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RecordingBook.Models
{
    public static class RecordSearch // Клас, в якому відбувається реалізація
                                     //методів пошуку за різними даними
    {
        private static Dictionary<string, string> searchFor = new
            Dictionary<string, string>();


        // Метод, що оновлює записи при зміні текстового поля.
        public static void AutoRefresh(ListBox listBox, string searchText)
        {
            if (searchText == "")
            {
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    ListBoxItem lBI = new ListBoxItem();
                    lBI = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(i);
                    lBI.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        
        // Метод, що шукає записи за заданими значеннями
        public static void SearchRecord(ListBox listBox, string searchText)
        {
            if (searchText == null) 
            { 
                return;
            }
            else
            {
                searchFor.Clear();
                searchText = searchText.ToLower();
                if (CheckAndGetParameters(searchText) == false)
                {
                    return;
                }

                bool containsSpecialCheck = searchText.Contains('@');

                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    ListBoxItem lBI = new ListBoxItem();
                    lBI = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(i);
                    Record? recordOfLBI = listBox.Items.GetItemAt(i) as Record;

                    if (recordOfLBI == null)
                    {
                        return;
                    }
                    if (containsSpecialCheck)
                    {
                        if (CheckData(searchFor, recordOfLBI))
                        {
                            lBI.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            lBI.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                    else if (!containsSpecialCheck)
                    {
                        if (recordOfLBI.FirstName.ToLower().Contains(searchText) ||
                            recordOfLBI.SecondName.ToLower().Contains(searchText) ||
                            recordOfLBI.LastName.ToLower().Contains(searchText))
                        {
                            lBI.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            lBI.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }

                }
            }

        }

        // Метод, що перевіряє чи задані параметри вірні.
        // Також в процесі змінює поле, якщо все вірно.
        private static bool CheckAndGetParameters(string searchText)
        {
            if (searchText.Count(s => s == '@') > 0)
            {
                List<string> searchTextList = searchText.Split('@').ToList();

                if (searchTextList.Count < 1)
                {
                    return false;
                }
                foreach (string s in searchTextList)
                {
                    if (s == "")
                    {
                        continue;
                    }
                    if (s[0] == ':' || s.Contains(':') == false)
                    {
                        return false;
                    }
                    List<string> innerList = s.Split(':').ToList();

                    if (innerList.Count < 2)
                    {
                        continue;
                    }
                    searchFor.Add(innerList[0].Trim().ToLower(),
                        innerList[1].Trim().ToLower());

                }

            }
            return true;

        }

        
        // Метод, що звіряє уведені користувачем "ключі" для пошуку
        // з реалізованими нижче.
        private static bool CheckData(Dictionary<string, string> finalParameters,
            Record currentRecord)
        {
            int isExist = finalParameters.Count;
            foreach (string key in finalParameters.Keys)
            {
                switch (key)
                {
                    case "ім'я":
                        if (currentRecord.FirstName.ToLower().Contains(
                            finalParameters[key]))
                        {
                            isExist--;
                        }
                        break;
                    case "ім`я":
                        if (currentRecord.FirstName.ToLower().Contains(
                            finalParameters[key]))
                        {
                            isExist--;
                        }
                        break;
                    case "прізвище":
                        if (currentRecord.SecondName.ToLower().Contains(
                            finalParameters[key]))
                        {
                            isExist--;
                        }
                        break;
                    case "побатькові":
                        if (currentRecord.LastName.ToLower().Contains(
                            finalParameters[key]))
                        {
                            isExist--;
                        }
                        break;
                    case "вік":
                        if (currentRecord.Age.ToLower().Contains(
                            finalParameters[key].ToLower()))
                        {
                            isExist--;
                        }
                        break;
                    case "країна":
                        if (currentRecord.Country.ToLower().Contains(
                            finalParameters[key]))
                        {
                            isExist--;
                        }
                        break;
                    case "місто":
                        if (currentRecord.City.ToLower().Contains(
                            finalParameters[key]))
                        {
                            isExist--;
                        }
                        break;
                }
            }
            if (isExist == 0)
            {
                return true;
            }
            return false;
        }



    }
}
