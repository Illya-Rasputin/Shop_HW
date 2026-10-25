using Microsoft.AspNetCore.Identity;
namespace Shop_HW.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Image { get; set; }
}
