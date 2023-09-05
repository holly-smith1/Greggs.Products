using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace Greggs.Products.Api.Models;

public class Product
{
    public string Name { get; set; }
    public decimal PriceInPounds { get; set; }


}