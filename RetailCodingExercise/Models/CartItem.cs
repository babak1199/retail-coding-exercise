using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailCodingExercise.Models
{
    public partial class CartItem
    {
        [Key]
        [StringLength(128)]
        public string ItemId { get; set; }
        public string CartId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Models.Product.CartItems))]
        public virtual Product Product { get; set; }
    }
}
