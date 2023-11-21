using EF_CRUD_REST_API;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //connecting to DB
        ConfigureServices(builder);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    public static void ConfigureServices(WebApplicationBuilder? builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddDbContext<AppDBContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));
    }
}