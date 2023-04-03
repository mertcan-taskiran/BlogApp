using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class Register
    {
        [Required]
        //[DisplayName("Adınız")]
        public string Name { get; set; }
        [Required]
        //[DisplayName("Soyadınız")]
        public string Surname { get; set; }
        [Required]
        //[DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }
        [Required]
        //[DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        //[DisplayName("Şifre")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        //[DisplayName("Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string RePassword { get; set; }

    }
}