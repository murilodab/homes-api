
using homes_api.Data;
using homes_api.Helpers;
using homes_api.Services;
using homes_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace homes_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = ConnectionHelper.GetConnectionString(builder.Configuration) ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Add dbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString,
            options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            //Add Services
            builder.Services.AddScoped<IHomeService, HomeService>();

            //Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("homes-angular", policy =>
                {
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

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.UseHttpsRedirection();

            app.UseCors("homes-angular");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
