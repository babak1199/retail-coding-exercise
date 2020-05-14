using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RetailCodingExercise.Models;
using RetailCodingExercise.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RetailCodingExercise.Controllers
{
    public class ProductsController : Controller
    {
        // TODO: Need to be used to log events and errors
        private readonly ILogger<ProductsController> _logger;

        private readonly ProductContext _productContext;
        private readonly ProductsConfig _productConfig;

        public ProductsController(ProductContext productContext, IOptionsMonitor<ProductsConfig> productsConfigAccessor, ILogger<ProductsController> logger)
        {
            _productContext = productContext;
            _productConfig = productsConfigAccessor.CurrentValue;
            _logger = logger;
        }

        // TODO: It would be nice to add caching to improve performance (with regard to product update interval)
        public async Task<IActionResult> Index([FromQuery] Search searchModel)
        {
            var cars = _productContext.Products
                .Where(p => p.Category.CategoryName == _productConfig.IncludedCategory);

            if(!searchModel.IsEmpty() && ModelState.IsValid)
            {
                cars = cars.Where(p => p.ProductName.Contains(searchModel.Query.ToLower()) || p.Description.Contains(searchModel.Query.ToLower()));
            }

            var errors = Enumerable.Empty<string>();
            if(!searchModel.IsEmpty() && !ModelState.IsValid)
            {
                errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            }

            var viewModel = new ProductViewModel
            {
                Title = _productConfig.IncludedCategory,
                ImagePath = _productConfig.ImagePath,
                Products = await cars.ToListAsync(),
                ErrorMessages = errors
            };

            return View(viewModel);
        }
    }
}