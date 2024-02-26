using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }

        public string Model { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }
    }
}
