using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HotelRoomDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter room name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter occupancy")]
        public int Occupancy { get; set; }

        [Range(1,3000, ErrorMessage = "The regular rate must be between 1 and 3000")]
        [Required]
        public double RegularRate { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string SqFt { get; set; }
        public virtual ICollection<HotelRoomImageDto> HotelRoomImages { get; set; }
        public List<string> ImageUrls { get; set; }

    }
}
