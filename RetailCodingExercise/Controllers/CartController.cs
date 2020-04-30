using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailCodingExercise.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RetailCodingExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ProductContext _context;

        public CartController(ProductContext context)
        {
            _context = context;
        }

        // TODO: The logic should be separated and clarified
        // TODO: Inputs need to be sanitized and requests need to be authorized plus other protections need to be added too.
        // POST: api/Cart
        [HttpPost]
        public async Task<ActionResult<CartItem>> PostItem([Bind("productId", "cartId")] CartItem cartItem)
        {
            var foundItem = await GetExistingCartItemExists(cartItem);

            if (foundItem == null)
            {
                cartItem.DateCreated = DateTime.Now;
                cartItem.Quantity = 1;
                cartItem.ItemId = Guid.NewGuid().ToString();

                _context.CartItems.Add(cartItem);
            }
            else
            {
                foundItem.DateCreated = DateTime.Now;
                foundItem.Quantity += 1;

                _context.Entry(foundItem).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (e is DbUpdateException && await CartItemExists(cartItem))
                {
                    return Conflict();
                }
                else if (e is DbUpdateConcurrencyException && !(await CartItemExists(foundItem)))
                {
                    return NotFound();
                }

                throw;
            }

            // In case of Add
            if (foundItem == null)
            {
                return CreatedAtAction("GetCartItem", new { id = cartItem.ItemId }, cartItem);
            }

            // In case of Update
            return NoContent();
        }

        private async Task<CartItem> GetExistingCartItemExists(CartItem cartItem)
        {
            return await duplicateCartItem(cartItem, _context).FirstOrDefaultAsync();
        }

        private async Task<bool> CartItemExists(CartItem cartItem)
        {
            return await duplicateCartItem(cartItem, _context).AnyAsync();
        }

        private readonly Func<CartItem, ProductContext, IQueryable<CartItem>> duplicateCartItem =
            (item, ctx) => ctx.CartItems.Where(
                c => c.ProductId == item.ProductId
                && c.CartId == item.CartId);
    }
}
