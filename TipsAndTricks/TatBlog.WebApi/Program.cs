

using TatBlog.WebApi.Endpoints;
using TatBlog.WebApi.Extensions;
using TatBlog.WebApi.Mapsters;
using TatBlog.WebApi.Validations;

var builder = WebApplication.CreateBuilder(args);
{
    builder.ConfigureSwaggerOpenApi()
         .ConfigureCors()
         .ConfigureNlog()
         .ConfifureServices()
         .ConfigureMapster()
         .ConfigureFluentValidation();
}


var app = builder.Build();
{
    app.SetupRequestpipeLine();

    app.MapAuthorEndpoints();

  app.MapCategoryEndpoints();

    app.Run();
}


app.Run();

