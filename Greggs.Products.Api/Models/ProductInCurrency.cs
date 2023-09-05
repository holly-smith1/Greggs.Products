using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace Greggs.Products.Api.Models;

public class ProductInCurrency
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public string CurrencyCode { get; set; }

}