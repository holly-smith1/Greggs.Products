namespace Greggs.Products.Api.Models
{
    public class Currency
    {
        public string CurrencyCode { get; private set; }
        public decimal ConversionRateFromGBP { get; private set; }
        public string CurrencyName { get; private set; }

        public Currency(string CurrencyCode, decimal ConversionRateFromGBP, string CurrencyName)
        {
            this.CurrencyCode = CurrencyCode;
            this.ConversionRateFromGBP = ConversionRateFromGBP;
            this.CurrencyName = CurrencyName;
        }
    }
}
