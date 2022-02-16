using Cinerva.Web.Models.Properties.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Cinerva.Web.Models.Properties
{
    public class BasePropertyViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " The Property needs a name!")]
        public string Name { get; set; }
        public decimal? Rating { get; set; }
        [DescriptionValidation(ErrorMessage = "The format of description is not valid\n use prefix : <Descrip&> and suffix : <$endOfDescrip>")]
        public string? Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int TotalRooms { get; set; }
        public int DaysForRefund { get; set; }
        public int PropertyTypeId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int AdminId { get; set; }
    }
}
