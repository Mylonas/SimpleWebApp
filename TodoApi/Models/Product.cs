namespace TodoApi.Models;

public class Product
{
    public long id { get; set; }
    public string? name { get; set; }
    public decimal price { get; set; }
    public string? category { get; set; }
}
