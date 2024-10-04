using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
        [Display(Name = "List Price")]
        [Range(1, 10000)]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1 - 50")]
        [Range(1, 10000)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for 51 - 100")]
        [Range(1, 10000)]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        [ValidateNever]
        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Package")]
        public int PackageId { get; set; }
        [ValidateNever]
        public Package Package { get; set; }



    }
}
