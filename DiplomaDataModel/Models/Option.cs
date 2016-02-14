using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Models
{
    public class Option
    {
        [Key]
        public int          OptionId { get; set; }

        [StringLength(50)]
        public String       Title { get; set; }

        public Boolean      IsActive { get; set; }
    }
}