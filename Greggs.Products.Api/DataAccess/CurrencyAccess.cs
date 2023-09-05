using Greggs.Products.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Greggs.Products.Api.DataAccess
{
    public class CurrencyAccess: IDataAccess_ByStringKey<Currency>
    {
        //I don't normally do this either!
        private static readonly IEnumerable<Currency> CurrencyDatabase = new List<Currency>()
        {
            new Currency("EUR", 1.11m, "Euro"),
            new Currency("GBP", 1.0m, "Pounds")
        };

        public Currency Get(string currencyCode)
        {
            return CurrencyDatabase.FirstOrDefault(c => c.CurrencyCode == currencyCode);
        }

    }

}
