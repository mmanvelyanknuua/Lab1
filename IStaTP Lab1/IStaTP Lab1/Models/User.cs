using Microsoft.AspNetCore.Identity;

namespace IStaTP_Lab1.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
    }
}
