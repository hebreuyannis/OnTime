using OnTime.Domain.Common;

namespace OnTime.Domain.User
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password {  get; set; }
        
        public List<Appointment> Appointments { get; set; }

        public User(Guid id, string firstname, string lastname, string email, string password):base(id) 
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Password = password;
        }

        private User() { }
    }
}
