using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Models
{
    public class Option
    {
        public int OptionId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Option Title is too long.")]
        public String Title { get; set; }

        [DisplayName("Is Active")]
        public Boolean IsActive { get; set; }
    }
}
