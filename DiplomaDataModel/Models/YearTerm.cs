using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Models
{
    public class YearTerm
    {
        public int YearTermId { get; set; }
        public int Year { get; set; }
        public int Term { get; set; }
        [DisplayName("Is Default")]
        public Boolean IsDefault { get; set; }
    }
}
