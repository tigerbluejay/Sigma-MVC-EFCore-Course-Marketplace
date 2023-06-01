using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Range(1, 10000)]
        public double Price { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

    }
}
