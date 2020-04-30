using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailCodingExercise.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("CategoryID")]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }
        public string Description { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
