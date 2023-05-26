using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecordingBook
{
    public class Record:INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        private string _firstName;
        private string _secondName;
        private string _lastName;
        private string _age;
        private string _streetAndNumber;
        private string _country;
        private string _city;
        private string _phoneNumber;
        private string _placeOfWorkStudy;
        public string FirstName
        {
            get 
            { 
                return _firstName; 
            }
            set 
            { 
                _firstName = value;
                if (Regex.IsMatch(_firstName, "[^a-zA-Z-а-яА-ЯіІїЇєЄёЁ]"))// ДОРОБИТИ
                {
                    AddError(nameof(FirstName), "Неправильно введене ім'я. Не має бути пробілів чи спеціальних символів ()");
                }
                else
                {
                    ClearErrors(nameof(FirstName));
                }
            }
        }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string StreetAndNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string PlaceOfWorkStudy { get; set; }





        public bool HasErrors => _propertyErrors.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_propertyErrors.ContainsKey(propertyName))
                return null;

            return _propertyErrors[propertyName];
        }


        public void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }

            _propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }
        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }



        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
