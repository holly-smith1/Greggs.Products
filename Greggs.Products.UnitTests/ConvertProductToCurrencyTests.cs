using FluentAssertions;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Utilities;
using System.Collections.Generic;
using Xunit;

namespace Greggs.Products.UnitTests;

public class ConvertProductToCurrencyTests
{
    

    [Fact]
    public void Convert_Should_Not_Affect_Name()
    {
        var productInput = new List<Product> { new() { Name="Product1", PriceInPounds=1.0m}
                                          };

        var currency = new Currency("EUR", 1.1m, "Test!");

        //Act
        var productOutput = ConvertProductsToCurrency.Convert(productInput, currency);

        //Assert
        productOutput[0].Name.Should().Be(productInput[0].Name);
    }

    [Fact]
    public void Convert_Should_Convert_Price_Using_Conversion_Rate_In_Currency()
    {
        var productInput = new List<Product> { new() { Name="Product1", PriceInPounds=1.0m} };

        var currency = new Currency("EUR", 1.1m, "Test!");

        //Act
        var productOutput = ConvertProductsToCurrency.Convert(productInput, currency);

        //Assert
        productOutput[0].Price.Should().Be(productInput[0].PriceInPounds * currency.ConversionRateFromGBP);
    }

    [Fact]
    public void Convert_Should_Return_Currency_Code_From_Currency()
    {
        var productInput = new List<Product> { new() { Name="Product1", PriceInPounds=1.0m} };

        var currency = new Currency("EUR", 1.1m, "Test!");

        //Act
        var productOutput = ConvertProductsToCurrency.Convert(productInput, currency);

        //Assert
        productOutput[0].CurrencyCode.Should().Be(currency.CurrencyCode);
    }

    [Fact]
    public void Convert_Should_Return_Converted_Price_To_Nearest_Cent()
    {
        var productInput = new List<Product> { new() { Name="Product1", PriceInPounds=1.5m} };

        var currency = new Currency("EUR", 1.11m, "Test!");

        var expectedPrice = System.Math.Round(productInput[0].PriceInPounds * currency.ConversionRateFromGBP,2);

        //Act
        var productOutput = ConvertProductsToCurrency.Convert(productInput, currency);

        //Assert
        productOutput[0].Price.Should().Be(expectedPrice);
    }


}