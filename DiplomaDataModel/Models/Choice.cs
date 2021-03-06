﻿using DiplomaDataModel.Models.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DiplomaDataModel.Models
{
    public class Choice
    {
        public int ChoiceId { get; set; }

        [Index("StudentId", IsUnique = true)]
        [Display(Name = "Student Number: ")]
        public String StudentId
        { get; set; }

        [Required]
        [Display(Name = "Year Term: ")]
        [ForeignKey("YearTerm")]
        public int? YearTermId { get; set; }
        [ForeignKey("YearTermId")]
        public YearTerm YearTerm { get; set; }

       
        [Required]
        [Display(Name = "First Name: ")]
        [StringLength(40, ErrorMessage = "Name is too long, 40 characters maximum")]
        public String StudentFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name: ")]
        [StringLength(40, ErrorMessage = "Name is too long, 40 characters maximum")]
        public String StudentLastName { get; set; }

        [Required]
        [CheckDuplicates("SecondChoiceOptionId", "ThirdChoiceOptionId", "FourthChoiceOptionId")]
        [Display(Name = "First Choice: ")]
        [ForeignKey("FirstOption")]
        public int? FirstChoiceOptionId { get; set; }
        [ForeignKey("FirstChoiceOptionId")]
        public Option FirstOption { get; set; }

        [Required]
        [CheckDuplicates("FirstChoiceOptionId", "ThirdChoiceOptionId", "FourthChoiceOptionId")]
        [Display(Name = "Second Choice: ")]
        [ForeignKey("SecondOption")]
        public int? SecondChoiceOptionId { get; set; }
        [ForeignKey("SecondChoiceOptionId")]
        public Option SecondOption { get; set; }

        [Required]
        [CheckDuplicates("FirstChoiceOptionId", "SecondChoiceOptionId", "FourthChoiceOptionId")]
        [Display(Name = "Third Choice: ")]
        [ForeignKey("ThirdOption")]
        public int? ThirdChoiceOptionId { get; set; }
        [ForeignKey("ThirdChoiceOptionId")]
        public Option ThirdOption { get; set; }

        [Required]
        [CheckDuplicates("FirstChoiceOptionId", "SecondChoiceOptionId", "ThirdChoiceOptionId")]
        [Display(Name = "Fourth Choice: ")]
        [ForeignKey("FourthOption")]
        public int? FourthChoiceOptionId { get; set; }
        [ForeignKey("FourthChoiceOptionId")]
        public Option FourthOption { get; set; }


        private DateTime _SelectionDate = DateTime.MinValue;

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyy}")]
        public DateTime SelectionDate {
            get
            {
                return (_SelectionDate == DateTime.MinValue) ? DateTime.Now : _SelectionDate;
            }
            set { _SelectionDate = value; }
        }

    }
}
