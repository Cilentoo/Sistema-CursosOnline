namespace Sistema_CursosOnline.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EType Role { get; set; }

        public User() { }

        public User(int id, string name, string email, string password, EType role)
        {
            Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = role;
        }
    }

    public enum EType 
    { 
        Instructor,
        Student 
    }
}
