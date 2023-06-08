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
    public class Record : INotifyDataErrorInfo  //Клас, що є реалізацією запису
                                                //і має відповідні властивості.
    {
        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        private string firstName;
        private string secondName;
        private string lastName;

        public Record()
        {
            FirstName = "FirstName";
            SecondName = "SecondName";
            LastName = "LastName";
            Age = "age";
            PhoneNumber = "+(number)";
            DateOfCreation= DateTime.Now;
            FormTheGreeting = false;
        }


        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                if (Regex.IsMatch(firstName, "[^a-zA-Z-а-яА-ЯіІїЇєЄёЁ]"))// ДОРОБИТИ
                {
                    AddError(nameof(FirstName), "Неправильно введене " +
                        "ім'я. Не має бути пробілів чи спеціальних " +
                        "символів та чисел, окрім '-'");
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
                return secondName;
            }
            set
            {
                secondName = value;
                if (Regex.IsMatch(secondName, "[^a-zA-Z-а-яА-ЯіІїЇєЄёЁ]"))
                {
                    AddError(nameof(SecondName), "Неправильно введене " +
                        "прізвище. Не має бути пробілів чи спеціальних " +
                        "символів та чисел, окрім '-'");
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
                return lastName;
            }
            set
            {
                lastName = value;
                if (Regex.IsMatch(lastName, "[^a-zA-Z-а-яА-ЯіІїЇєЄёЁ]"))
                {
                    AddError(nameof(LastName), "Неправильно введене " +
                        "по-батькові. Не має бути пробілів чи спеціальних " +
                        "символів та чисел, окрім '-'");
                }
                else
                {
                    ClearErrors(nameof(LastName));
                }
            }
        }
        public string Age { get; set; }
        public string StreetAndNumber { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
        public string PhoneNumber { get; set; }
        public string PlaceOfWorkStudy { get; set; } = default!;
        public string AdditionalInfo { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public DateTime DateOfCreation { get; set; }
        public bool FormTheGreeting { get; set; }


        public bool HasErrors => _propertyErrors.Any();


        public static int GetAge(DateTime? BirthDate)
        {
            if (BirthDate == null)
            {
                return 0;
            }
            DateTime notNull_BirthDate = (DateTime)BirthDate;
            DateTime currentTime = DateTime.Now;
            int age = currentTime.Year - notNull_BirthDate.Year;
            if (notNull_BirthDate > currentTime.AddYears(-age))
            {
                age--;
            }
            return age;
        }


        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) ||
                !_propertyErrors.ContainsKey(propertyName))
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
            ErrorsChanged?.Invoke(this,
                new DataErrorsChangedEventArgs(propertyName));
        }


        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
