using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedCrossItCheckingSystem.Models
{
    public class UserData
    {
        //private fields with required attribute
        [Required]
        private string userName;
        [Required]
        private string password;

        //public properties
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
    }
}
