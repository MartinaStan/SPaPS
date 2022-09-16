﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SPaPS.Models.AccountModels
{
    public class RegisterModel
    {
        [Required (AllowEmptyStrings = false, ErrorMessage = "Полето е задолжително")]
        [DisplayName ("Е-маил")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName ("Телефон")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Тип на корисник")]
        public int ClientTypeId { get; set; }
        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Име и презиме")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName ("Адреса")]
        public string Address { get; set; } = null!;
        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName ("Ембг")]
        public string IdNo { get; set; } = null!;
        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName("Град")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "Полето е задолжително")]
        [DisplayName ("Држава")]
        public int? CountryId { get; set; }
    }
}