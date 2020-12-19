using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PavlovWebApi.Models
{
    public class CustomerData
    {
        public string Name { get; set; } // Имя клиента
        public string Surname { get; set; } // Фамилия
        public bool Gender { get; set; } // Пол, 0 - Ж, 1 - М
        public DateTime VisitDate { get; set; } // Дата и время визита
        public string Service { get; set; } // Услуга
        public int Cost { get; set; } // Стоимость
        public string Phone { get; set; } // Телефонный номер
        public BaseModelValidationResult Validate()
        {
            var validationResult = new BaseModelValidationResult();

            if (string.IsNullOrWhiteSpace(Name)) validationResult.Append($"Имя не может быть пустым");
            if (string.IsNullOrWhiteSpace(Surname)) validationResult.Append($"Фамилия не может быть пустой");
            if((DateTime.Now-VisitDate).TotalDays > 180) validationResult.Append($"Слишком давняя дата (более 6 месяцев назад)");
            if (!string.IsNullOrEmpty(Name) && !char.IsUpper(Name.FirstOrDefault())) validationResult.Append($"Имя {Name} должно начинаться с большой буквы");
            if (!string.IsNullOrEmpty(Surname) && !char.IsUpper(Surname.FirstOrDefault())) validationResult.Append($"Фамилия {Surname} Должна начинаться с большой буквы");
            if (string.IsNullOrWhiteSpace(Service)) validationResult.Append($"Информация об оказанной услуге не может быть пустой");
            if (Phone.Length != 11) validationResult.Append($"Телефон {Phone} должен содержать 11 символов, например 79993334455");

            return validationResult;
        }


        public override string ToString()
        {
            return $"{Name} {Surname}, {Gender}, {Phone}. {VisitDate}: {Service}, {Cost}";
        }

    }
}

