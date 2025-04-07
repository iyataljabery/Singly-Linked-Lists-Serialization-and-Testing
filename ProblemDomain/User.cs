namespace UnitTest.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        // Override Equals and GetHashCode for correct comparison in tests
        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Id == user.Id &&
                   Name == user.Name &&
                   Email == user.Email &&
                   Password == user.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Email, Password);
        }
    }
}

