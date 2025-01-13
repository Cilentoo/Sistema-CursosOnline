namespace Sistema_CursosOnline.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public EType Role { get; set; }

        public string Status { get; set; }

        public User() { }

        public User(int id, string name, string email, string passwordHash, EType role , String status)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            Status = status;
        }

        public void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool ValidatePassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }

    public enum EType 
    { 
        Instructor = 0,
        Student = 1
    }
}
