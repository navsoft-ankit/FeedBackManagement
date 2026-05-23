using Authservice.Data;
public class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.Add(new User
            {
                Name = "Admin",
                Email = "Admin@example.com",
                Password = "Admin123",
                Role = "Admin"
            });
            await context.SaveChangesAsync();
        }
    }
}