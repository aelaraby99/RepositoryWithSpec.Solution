using Microsoft.EntityFrameworkCore;
using RepositorySpec.API.Data;
using RepositorySpec.API.Data.DataSeed;
using RepositorySpec.API.Repository.Classes;
using RepositorySpec.API.Repository.Interfaces;

namespace RepositorySpec.API
{
    public class Program
    {
        public static async Task Main( string [] args )
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(
                options => options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection") ,
                b => b.MigrationsAssembly(typeof(StoreContext).Assembly.FullName)));
            //builder.Services.AddDbContext<StoreContext>(
            //    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ,
            //    b => b.MigrationsAssembly(typeof(StoreContext).Assembly.FullName)));

            //builder.Services.AddScoped(typeof(IRepository<>) , typeof(Repository<>));
            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var provider = scope.ServiceProvider;
            var logger = provider.GetRequiredService<ILogger<Program>>();
            var context = provider.GetRequiredService<StoreContext>();
            
            //
            var appliedMigrations = context.Database.GetAppliedMigrations().ToHashSet();
            var allMigrations = context.Database.GetMigrations().ToHashSet();

            // Check if there are any pending migrations
            var pendingMigrations = allMigrations.Except(appliedMigrations).Any();
            if (pendingMigrations)
            {
                try
                {
                    await context.Database.MigrateAsync();
                    logger.LogInformation("Migrations applied successfully.");
                    DataSeeding.SeedData(context);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex , "Error applying migrations");
                }
            }

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
