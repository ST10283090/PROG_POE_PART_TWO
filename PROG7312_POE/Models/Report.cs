using System.ComponentModel.DataAnnotations;

namespace PROG7312_POE.Models
{
    public class Report
    {
        //part one feeback, location more specific
        public int ReportId { get; set; }

        [Required(ErrorMessage = "Street address is required")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Suburb is required")]
        public string Suburb { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string ReportCategory { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string ReportDescription { get; set; }

        public string? ReportDocument { get; set; }
    }
}