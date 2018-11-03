using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Helpdesk.ViewModel
{
    public class ProvRegViewModel
    {
        public int Id { get; set; }
        public int Provinces_Id { get; set; }
        public int Regencies_Id { get; set; }
        public int Districts_Id { get; set; }
        public int Villages_Id { get; set; }
        public int Departments_Id { get; set; }
        public int Roles_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostNumber { get; set; }
        public string Email { get; set; }

        public ProvRegViewModel() { }
        public ProvRegViewModel(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            Gender = user.Gender;
            Password = user.Password;
            PhoneNumber = user.PhoneNumber;
            Address = user.Address;
            PostNumber = user.PostNumber;
            Email = user.Email;
            Villages_Id = Convert.ToInt32(user.Villages_Id);
            Roles_Id = Convert.ToInt32(user.Roles_Id);
            Departments_Id = Convert.ToInt32(user.Departments_Id);
        }
    }
}