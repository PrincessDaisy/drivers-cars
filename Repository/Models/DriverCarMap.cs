using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class DriverCarMap
    {
        [Key]
        public int Id { get; set; }

        public Driver Driver { get; set; }

        public Car Car { get; set; }
    }
}
