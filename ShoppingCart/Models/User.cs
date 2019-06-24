using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //[Remote("CheckUsername", "Login", HttpMethod = "Post", ErrorMessage = "Username is incorrect")]
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        //[Remote("CheckPassword", "Login", HttpMethod = "Post", ErrorMessage = "Password is incorrect")]
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string SessionId { get; set; }
        public string Salt { get; set; }
    }
}