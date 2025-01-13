using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Application.Request
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } 
        public string Password { get; set; }
    }
}
