
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Serilog;
using TodoServices.Interfaces;
using TodoServices.Models;
using TodoServices.Services;

namespace TodoServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //for each service add seperate builder lines as below
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILabelService, LabelService>();

            //builder.Services.AddControllers();
            builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>{options.SerializerSettings.ContractResolver = null;});

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Set minimum log level
                .WriteTo.Console() // Log to console
                .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day) // Log to file
                .CreateLogger();

            builder.Host.UseSerilog(); // Use Serilog for logging

            //part of injection of dbcontext
            var connectionString = builder.Configuration.GetConnectionString("TodoContext");
            builder.Services.AddDbContext<TodoContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
