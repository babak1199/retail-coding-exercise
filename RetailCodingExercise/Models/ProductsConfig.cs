namespace RetailCodingExercise.Models
{
    public class ProductsConfig
    {
        public ProductsConfig()
        {
            // Default values
            IncludedCategory = "Cars";
            ImagePath = "/img/cars/";
        }

        public string IncludedCategory { get; set; }
        public string ImagePath { get; set; }
    }
}
