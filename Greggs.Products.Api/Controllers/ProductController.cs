using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IDataAccess<Product> _productAccess;
    private readonly IDataAccess_ByStringKey<Currency> _currencyAccess;

    public ProductController(ILogger<ProductController> logger, IDataAccess<Product> productAccess, IDataAccess_ByStringKey<Currency> currencyAccess)
    {
        _logger = logger;
        _productAccess = productAccess;
        _currencyAccess = currencyAccess;   
    }

    [HttpGet]
    public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5)
    {
        return _productAccess.List(pageStart, pageSize);
    }


    [HttpGet("~/ProductInCurrency")]
    public IActionResult ProductInCurrency(int pageStart = 0, int pageSize = 5, string currencyCode = "GBP")
    {
        var products = _productAccess.List(pageStart, pageSize);
        var currency = _currencyAccess.Get(currencyCode);
        if (currency != null)
        {
            return Ok( ConvertProductsToCurrency.Convert(products, currency));
        }
        return BadRequest();

    }

}