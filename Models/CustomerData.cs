﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PavlovWebApi.Models
{
    public class CustomerData
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } // Имя клиента
        public string Surename { get; set; } // Фамилия
        public int Age { get; set; } // Возраст
        public DateTime VisitDate { get; set; } // Дата и время визита
        public string Service { get; set; } // Услуга
        public int Cost { get; set; } // Стоимость
        public string Phone { get; set; } // Телефонный номер
        public BaseModelValidationResult Validate()
        {
            var validationResult = new BaseModelValidationResult();
            if(Age > 100 || Age < 18) validationResult.Append($"Некорректно указан возраст");
            if (string.IsNullOrWhiteSpace(Name)) validationResult.Append($"Имя не может быть пустым");
            if (string.IsNullOrWhiteSpace(Surename)) validationResult.Append($"Фамилия не может быть пустой");
            if((DateTime.Now-VisitDate).TotalDays > 180) validationResult.Append($"Слишком давняя дата (более 6 месяцев назад)");
            if (!string.IsNullOrEmpty(Name) && !char.IsUpper(Name.FirstOrDefault())) validationResult.Append($"Имя {Name} должно начинаться с большой буквы");
            if (!string.IsNullOrEmpty(Surename) && !char.IsUpper(Surename.FirstOrDefault())) validationResult.Append($"Фамилия {Surename} Должна начинаться с большой буквы");
            if (string.IsNullOrWhiteSpace(Service)) validationResult.Append($"Информация об оказанной услуге не может быть пустой");
            if (Phone.Length != 11) validationResult.Append($"Телефон {Phone} должен содержать 11 символов, например 79993334455");

            return validationResult;
        }


        public override string ToString()
        {
            return $"{Name} {Surename}, {Age}, {Phone}. {VisitDate}: {Service}, {Cost}";
        }

    }
}

