using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Greggs.Products.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwaggerGen();

        services.AddSingleton<IDataAccess<Product>, ProductAccess>();

        services.AddSingleton<IDataAccess_ByStringKey<Currency>, CurrencyAccess>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Greggs Products API V1"); });
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.ApplicationServices.GetRequiredService<IDataAccess<Product>>();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}