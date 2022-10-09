using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SPaPS.Models
{
    public partial class Request
    {
        public long RequestId { get; set; }

        [DisplayName("Датум на поднесување на барањето")]
        public DateTime RequestDate { get; set; }

        [DisplayName("Сервис")]
        public long ServiceId { get; set; }
        //public string? Description { get; set; }
        public long? ContractorId { get; set; }

        [DisplayName("Тип на објект")]
        public int? BuildingTypeId { get; set; }

       
        //public string? DescriptionB { get; set; }

        [DisplayName("Големина на објект")]
        public int? BuildingSize { get; set; }

        [DisplayName("Од:")]
        public DateTime? FromDate { get; set; }

        [DisplayName("До:")]
        public DateTime? ToDate { get; set; }

        [DisplayName("Боја")]
        public string? Color { get; set; }

        [DisplayName("Број на теписи")]
        public int? NoOfCarpets { get; set; }

        [DisplayName("Број на сијалици")]
        public int? NoOfLights { get; set; }

        [DisplayName("Број на штекери")]
        public int? NoOfSockets { get; set; }

        [DisplayName("Број на врати")]
        public int? NoOfDoors { get; set; }

        [DisplayName("Број на прозорци")]
        public int? NoOfWindows { get; set; }

        [DisplayName("Забелешка:")]
        public string? Note { get; set; }

        [DisplayName("Забелешка:")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Забелешка:")]
        public int CreatedBy { get; set; }

        [DisplayName("Ажуриран на:")]
        public DateTime? UpdatedOn { get; set; }

        [DisplayName("Ажуриран од:")]
        public int? UpdatedBy { get; set; }

        [DisplayName("Активен:")]
        public bool? IsActive { get; set; }

        [DisplayName("Сервис")]

        public virtual Service? Service { get; set; } = null!;
    }
}
