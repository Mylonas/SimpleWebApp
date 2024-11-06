namespace TodoApi.Models;

public class Product
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public decimal price { get; set; }
    public string? category { get; set; }
}
