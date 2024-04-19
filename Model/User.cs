using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace FoodApp.Model
{
    [Index(nameof(Login), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
