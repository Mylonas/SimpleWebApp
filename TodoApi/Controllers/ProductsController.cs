using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly TodoContext _context;

    public ProductsController(TodoContext context)
    {
        _context = context;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(long id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound($"Product with id {id} not found.");

        return product;
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(long id, Product product)
    {
        if (id != product.Id)
            return BadRequest("Route id does not match product id.");

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
                return NotFound($"Product with id {id} not found.");
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(long id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return NotFound($"Product with id {id} not found.");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/Products/ByPriceRange?min=10&max=100
    [HttpGet("ByPriceRange")]
    public async Task<ActionResult<IEnumerable<Product>>> GetByPriceRange(decimal min, decimal max)
    {
        if (min < 0 || max < 0 || min > max)
            return BadRequest("Invalid price range. min and max must be positive and min must be <= max.");

        var products = await _context.Products
            .Where(p => p.Price >= min && p.Price <= max)
            .ToListAsync();

        if (!products.Any())
            return NotFound("No products found within the specified price range.");

        return Ok(products);
    }

    private bool ProductExists(long id) =>
        _context.Products.Any(e => e.Id == id);
}
