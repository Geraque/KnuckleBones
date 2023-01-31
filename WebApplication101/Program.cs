using Microsoft.EntityFrameworkCore;
using WebApplication101;
using WebApplication101.EfCore;

internal partial class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        // Add services to the container.
        builder.Services.AddDbContext<EF_DataContext>(
                        o => o.UseNpgsql(builder.Configuration.GetConnectionString("Ef_Postgres_Db"))
                    );
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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

        //KnucleBones game = new KnucleBones();
        //game.whoseStep();

        app.Run();


    }
}