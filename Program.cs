
using Microsoft.EntityFrameworkCore;
using Simple_Product_Management_System.Data.DBContext;
using Simple_Product_Management_System.Dto;
using FluentValidation;
using FluentValidation.AspNetCore;
using Simple_Product_Management_System.BLL.Product;
using Simple_Product_Management_System.BLL.Helpper;
namespace Simple_Product_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddValidatorsFromAssemblyContaining<ProductDTO>();

            builder.Services.AddScoped<IValidator<ProductDTO>, ProductValidator>();

            builder.Services.AddScoped<IProdect, ProductReo>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    // Allow all origins, methods, and headers (you can configure this further)
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.Run();
        }
    }
}
