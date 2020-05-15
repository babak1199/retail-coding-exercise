using System.Collections.Generic;

namespace RetailCodingExercise.Models
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
    }
}
