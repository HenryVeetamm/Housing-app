namespace App.DAL.EF.AppData;

public class InitialData
{
    public static string[] Roles { get; } = { "User", "Admin"};

    public static readonly (string email, string FirstName, string LastName, string password, string? role)[] Users =
        {
            ("user1@ttu.ee", "user1", "user1", "Admin,1", "User"),
            ("user2@ttu.ee", "user2", "user2", "Admin,1", "User"),
            ("admin1@ttu.ee", "admin1", "admin1", "Admin,1", "Admin"),
            ("admin2@ttu.ee", "admin2", "admin2", "Admin,1", "Admin"),
        };

    public static readonly string[] Foos =
    {
        "Juri",
        "Mari",
        "Taavi",
        "MINA",
        "SINA"
    };
}