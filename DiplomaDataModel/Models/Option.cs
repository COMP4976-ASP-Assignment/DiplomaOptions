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

        [MaxLength(50)]
        public String       Title { get; set; }

        public Boolean      IsActive { get; set; }

        public List<Choice> Choices { get; set; }
    }
}

/*  
    Title	            IsActive
    Data Communications	 True
    Client Server	     True
    Digital Processing	 True
    Information Systems	 True
    Database	         False
    Web & Mobile	     True
    Tech Pro	         False

*/
