using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecordingBook.Models
{
    public class Record : INotifyDataErrorInfo
    {
        public Record()
        {
            FirstName = "FirstName";
            SecondName = "SecondName";
            LastName = "LastName";
            Age = "age";
            PhoneNumber = "+(number)";
            dateOfCreation= DateTime.Now;

            recordID = new Random().Next();
        }
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
        private string _additionalInfo;
        private DateTime _dateOfCreation;
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
                    AddError(nameof(FirstName), "Неправильно введене ім'я. Не має бути пробілів чи спеціальних символів, окрім '-'");
                }
                else
                {
                    ClearErrors(nameof(FirstName));
                }
            }
        }
        public string SecondName
        {
            get
            {
                return _secondName;
            }
            set
            {
                _secondName = value;
                if (Regex.IsMatch(_secondName, "[^a-zA-Z-а-яА-ЯіІїЇєЄёЁ]"))// ДОРОБИТИ
                {
                    AddError(nameof(SecondName), "Неправильно введене прізвище. Не має бути пробілів чи спеціальних символів, окрім '-'");
                }
                else
                {
                    ClearErrors(nameof(SecondName));
                }
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                if (Regex.IsMatch(_lastName, "[^a-zA-Z-а-яА-ЯіІїЇєЄёЁ]"))// ДОРОБИТИ
                {
                    AddError(nameof(LastName), "Неправильно введене по-батькові. Не має бути пробілів чи спеціальних символів, окрім '-'");
                }
                else
                {
                    ClearErrors(nameof(LastName));
                }
            }
        }
        public string Age { get; set; }
        public string StreetAndNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string PlaceOfWorkStudy { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public DateTime dateOfCreation { get; set; }
        public int recordID { get; set; }





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


        public static int GetAge(DateTime? BirthDate)
        {
            if (BirthDate == null) return 0;
            DateTime notNull_BirthDate = (DateTime)BirthDate;
            int age = 0;
            DateTime currentTime = DateTime.Now;
            age = currentTime.Year - notNull_BirthDate.Year;
            if (notNull_BirthDate > currentTime.AddYears(-age)) age--;
            return age;
        }
    }
}
