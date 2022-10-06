using System.ComponentModel;

namespace SPaPS.Models.CustomModels
{
    public class vm_Service
    {
        public long ServiceId { get; set; }

        [DisplayName("Сервис")]
        public string? Description { get; set; }
        public List<int> ActivityIds { get; set; } = new List<int>();

        [DisplayName("Активности")]
        public string? Activities { get; set; }
    }
}