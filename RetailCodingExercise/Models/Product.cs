using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailCodingExercise.Models
{
    public partial class Product
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
        }

        [Key]
        [Column("ProductID")]
        public int ProductId { get; set; }
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public double? UnitPrice { get; set; }
        [Column("CategoryID")]
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(Models.Category.Products))]
        public virtual Category Category { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
