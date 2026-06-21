using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Controllers;
using TodoApi.Models;

namespace TodoApi.Tests;

public class ProductsControllerTests
{
    private static TodoContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new TodoContext(options);
    }

    [Fact]
    public async Task GetProducts_ReturnsAllProducts()
    {
        using var context = CreateContext();
        context.Products.AddRange(
            new Product { Id = 1, Name = "Apple", Price = 1.50m, Category = "Fruit" },
            new Product { Id = 2, Name = "Banana", Price = 0.75m, Category = "Fruit" }
        );
        await context.SaveChangesAsync();

        var controller = new ProductsController(context);
        var result = await controller.GetProducts();

        var items = Assert.IsAssignableFrom<IEnumerable<Product>>(result.Value);
        Assert.Equal(2, items.Count());
    }

    [Fact]
    public async Task GetProduct_ReturnsProduct_WhenExists()
    {
        using var context = CreateContext();
        context.Products.Add(new Product { Id = 1, Name = "Apple", Price = 1.50m, Category = "Fruit" });
        await context.SaveChangesAsync();

        var controller = new ProductsController(context);
        var result = await controller.GetProduct(1);

        Assert.Equal("Apple", result.Value?.Name);
    }

    [Fact]
    public async Task GetProduct_ReturnsNotFound_WhenMissing()
    {
        using var context = CreateContext();
        var controller = new ProductsController(context);

        var result = await controller.GetProduct(99);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task PostProduct_CreatesProduct()
    {
        using var context = CreateContext();
        var controller = new ProductsController(context);
        var product = new Product { Id = 1, Name = "Orange", Price = 2.00m, Category = "Fruit" };

        var result = await controller.PostProduct(product);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("Orange", ((Product)created.Value!).Name);
        Assert.Equal(1, await context.Products.CountAsync());
    }

    [Fact]
    public async Task DeleteProduct_RemovesProduct()
    {
        using var context = CreateContext();
        context.Products.Add(new Product { Id = 1, Name = "Apple", Price = 1.50m, Category = "Fruit" });
        await context.SaveChangesAsync();

        var controller = new ProductsController(context);
        await controller.DeleteProduct(1);

        Assert.Equal(0, await context.Products.CountAsync());
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNotFound_WhenMissing()
    {
        using var context = CreateContext();
        var controller = new ProductsController(context);

        var result = await controller.DeleteProduct(99);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetByPriceRange_ReturnsMatchingProducts()
    {
        using var context = CreateContext();
        context.Products.AddRange(
            new Product { Id = 1, Name = "Cheap", Price = 5m, Category = "A" },
            new Product { Id = 2, Name = "Mid", Price = 50m, Category = "A" },
            new Product { Id = 3, Name = "Expensive", Price = 200m, Category = "A" }
        );
        await context.SaveChangesAsync();

        var controller = new ProductsController(context);
        var result = await controller.GetByPriceRange(10m, 100m);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var items = Assert.IsAssignableFrom<IEnumerable<Product>>(ok.Value);
        Assert.Single(items);
        Assert.Equal("Mid", items.First().Name);
    }

    [Fact]
    public async Task GetByPriceRange_ReturnsBadRequest_ForInvalidRange()
    {
        using var context = CreateContext();
        var controller = new ProductsController(context);

        var result = await controller.GetByPriceRange(100m, 10m);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetByPriceRange_ReturnsNotFound_WhenNoMatch()
    {
        using var context = CreateContext();
        context.Products.Add(new Product { Id = 1, Name = "Expensive", Price = 500m, Category = "A" });
        await context.SaveChangesAsync();

        var controller = new ProductsController(context);
        var result = await controller.GetByPriceRange(1m, 10m);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task PutProduct_ReturnsBadRequest_WhenIdMismatch()
    {
        using var context = CreateContext();
        var controller = new ProductsController(context);

        var result = await controller.PutProduct(1, new Product { Id = 2, Name = "X", Price = 1m });

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
