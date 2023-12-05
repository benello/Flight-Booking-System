using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels;

public class CreateFlightViewModel
{
    [Required]
    public int Rows { get; set; }

    [Required]
    public int Columns { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Departure Date")]
    public DateTime DepartureDate { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Arrival Date")]
    public DateTime ArrivalDate { get; set; }

    [Required]
    [Display(Name = "Country From")]
    public string CountryFrom { get; set; } = null!;

    [Required]
    [Display(Name = "Country To")]
    public string CountryTo { get; set; } = null!;
    
    [Required]
    [Display(Name = "Whole Sale Price")]
    [Range(0, double.MaxValue)]
    public double WholeSalePrice { get; set; }
    
    [Display(Name = "Commission Rate")]
    [Range(0, double.MaxValue)]
    public double? CommissionRate { get; set; }
}