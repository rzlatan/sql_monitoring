using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SQLMonitoring.Model
{
    public enum UserType
    {
        ADMIN,
        CLIENT
    }

    public class User
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    
        public UserType AccountType { get; set; } 

        public bool EmailConfirmed { get; set; }

        public List<Server> ServerList { get; set; }

        public List<Report> ReportList { get; set; }
    }
}