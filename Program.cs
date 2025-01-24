using Microsoft.EntityFrameworkCore;
using RadocInvoice.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        var app = builder.Build();

        // Populate DB with dummy information
        using (var scope = app.Services.CreateScope())
        {
            Seed(scope.ServiceProvider.GetRequiredService<AppDbContext>());
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }

    public static void Seed(AppDbContext context)
    {
        if (!context.Clients.Any())
        {
            context.Clients.AddRange(
                new Client { Name = "John Willow", SocialSecurityNumber = "1234567891011", PhoneNumber = "0799123444" },
                new Client { Name = "Jane Smith", SocialSecurityNumber = "9876543211011" }
            );

            context.SaveChanges();
        }

        if (!context.Doctors.Any())
        {
            var doc1 = new Doctor { Name = "Robin Walliams", PracticeLicenseNumber = "RU14XU128383", Email = "robin.w@gmail.com" };
            var doc2 = new Doctor { Name = "Donald Duke", PracticeLicenseNumber = "CJ1288481284" };
            context.Doctors.AddRange(doc1, doc2);

            context.SaveChanges();

            if (!context.DoctorContracts.Any())
            {
                context.DoctorContracts.AddRange(
                    new DoctorContract { DoctorId = doc1.Id, ContractExpirationDate = DateTime.Today },
                    new DoctorContract { DoctorId = doc2.Id, ContractExpirationDate = DateTime.Parse("2027-01-01") }
                );

                context.SaveChanges();
            }
        }

        if (!context.Services.Any())
        {
            context.Services.AddRange(
                new Service { Name = "Therapy Session", Details = "CBT Based, 1h" },
                new Service { Name = "Heart EKG", Details = "30 min" },
                new Service { Name = "Tooth Removal", Details = "hurts, 50 min"}
            );

            context.SaveChanges();
        }

    }
}