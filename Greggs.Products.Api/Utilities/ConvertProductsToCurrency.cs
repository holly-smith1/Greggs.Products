using System;
using System.Collections.Generic;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Utilities
{
    internal static class ConvertProductsToCurrency
    {
        internal static List<ProductInCurrency> Convert(IEnumerable<Product> products, Currency currency)
        {
            List<ProductInCurrency> result = new List<ProductInCurrency>();
            foreach (var product in products)
            {
                result.Add(new ProductInCurrency()
                {
                    CurrencyCode = currency.CurrencyCode,
                    Name = product.Name,
                    Price = Math.Round(product.PriceInPounds * currency.ConversionRateFromGBP, 2)
                });
            }

            return result;
        }
    }
}
