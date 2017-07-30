namespace Snowinmars.Entities
{
    public class User : Entity
    {
        public User(string username) : base()
        {
            this.Username = username;
        }

        public string Email { get; set; }
        public Language Language { get; set; }
        public string PasswordHash { get; set; }
        public UserRoles Roles { get; set; }
        public string Salt { get; set; }
        public string Username { get; set; }
    }
}