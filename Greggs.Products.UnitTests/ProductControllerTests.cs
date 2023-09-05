using FluentAssertions;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Greggs.Products.UnitTests;

public class ProductControllerTests
{
    private readonly ILogger<ProductController> _logger;
    private readonly ProductController _productController;
    private readonly IDataAccess<Product> _productAccess;
    private readonly IDataAccess_ByStringKey<Currency> _currencyAccess;

    public ProductControllerTests()
    {
        //Arrange
         _productAccess = Substitute.For<IDataAccess<Product>>();
         _logger = Substitute.For<ILogger<ProductController>>();
        _currencyAccess = Substitute.For<IDataAccess_ByStringKey<Currency>>();
        _productController = new ProductController(_logger, _productAccess,_currencyAccess);
    }

    [Fact]
    public void Calling_Get_Should_Call_ProductAccess_List()
    {

        //Act
        _productController.Get();

        //Assert
        _productAccess.Received().List(Arg.Any<int?>(), Arg.Any<int?>());
    }

    [Fact]
    public void Calling_Get_WithSpecifiedPageSize_Should_Call_ProductAccess_List_With_Expected_PageSize()
    {
        var PageSize = 10;

        //Act
        _productController.Get(pageSize:PageSize);

        //Assert
        _productAccess.Received().List(Arg.Any<int?>(), PageSize);
    }

    [Fact]
    public void Calling_Get_WithSpecifiedPageStart_Should_Call_ProductAccess_List_With_Expected_PageStart()
    {

        var PageStart = 3;

        //Act
        _productController.Get(pageStart: PageStart);

        //Assert
        _productAccess.Received().List(PageStart,Arg.Any<int?>());
    }


    [Fact]
    public void Calling_ProductInCurrency_Should_Call_ProductAccess_List()
    {

        //Act
        _productController.ProductInCurrency();

        //Assert
        _productAccess.Received().List(Arg.Any<int?>(), Arg.Any<int?>());
    }

    [Fact]
    public void Calling_ProductInCurrency_WithSpecifiedPageSize_Should_Call_ProductAccess_List_With_Expected_PageSize()
    {
        var PageSize = 10;

        //Act
        _productController.ProductInCurrency(pageSize: PageSize);

        //Assert
        _productAccess.Received().List(Arg.Any<int?>(), PageSize);
    }

    [Fact]
    public void Calling_ProductInCurrency_WithSpecifiedPageStart_Should_Call_ProductAccess_List_With_Expected_PageStart()
    {

        var PageStart = 3;

        //Act
        _productController.ProductInCurrency(pageStart: PageStart);

        //Assert
        _productAccess.Received().List(PageStart, Arg.Any<int?>());
    }

    [Fact]
    public void Calling_ProductInCurrency_WithNoCurrencyCode_Should_Call_CurrencyAccess_With_GBP()
    {
        //Act
        _productController.ProductInCurrency();

        //Assert
        _currencyAccess.Received().Get("GBP");
    }

    [Fact]
    public void Calling_ProductInCurrency_With_EUR_CurrencyCode_Should_Call_CurrencyAccess_With_EUR()
    {

        //Act
        _productController.ProductInCurrency(currencyCode:"EUR");

        //Assert
        _currencyAccess.Received().Get("EUR");
    }

    [Fact]
    public void Calling_ProductInCurrency_With_Valid_CurrencyCode_Should_Return_BadResult()
    {
       //Arrange
        _currencyAccess.Get("GBP").Returns(new Currency("GBP", 1.0m, "Pounds"));

        //Act
        var result = _productController.ProductInCurrency(currencyCode: "GBP");

        //Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public void Calling_ProductInCurrency_With_Invalid_CurrencyCode_Should_Return_BadResult()
    {
        //Arrange
        _currencyAccess.Get("XXX").ReturnsNull();

        //Act
        var result = _productController.ProductInCurrency(currencyCode: "XXX");

        //Assert
        result.Should().BeOfType <BadRequestResult>();
    }

}