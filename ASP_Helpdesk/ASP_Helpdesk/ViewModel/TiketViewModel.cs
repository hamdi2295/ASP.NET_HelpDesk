using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Helpdesk.ViewModel
{
    public class TiketViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Techinician { get; set; }
        public string AdminBy { get; set; }
        public string DueDateTiket { get; set; }
        public int DueDates_Id { get; set; }
        public string Sub_Categories_Id { get; set; }
        public string Categories_Id { get; set; }
        public string users_Id { get; set; }
        public string Status { get; set; }
        public string Client { get; set; }
        public string Category { get; set; }
        public string SubCat { get; set; }
        public string datewait { get; set; }
        public string datehold { get; set; }
        public string dateclose { get; set; }

        public TiketViewModel() { }
        public TiketViewModel(Ticket tiket)
        {
            Id = tiket.Id;
            Description = tiket.Description;
            Techinician = tiket.Technician;
            AdminBy = tiket.AdminBy;
            DueDateTiket = tiket.DueDate.ToString();
            DueDates_Id = Convert.ToInt32(tiket.DueDates_Id);
            Sub_Categories_Id = tiket.SubCategory.Id.ToString(); ;           
            users_Id = tiket.users_Id.ToString();
            
            Client = tiket.User.FirstName;
            Category = tiket.SubCategory.Category.Name;
            SubCat = tiket.SubCategory.Name;
            
        }

    }
}