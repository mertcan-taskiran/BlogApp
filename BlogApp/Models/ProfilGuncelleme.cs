using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class ProfilGuncelleme
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
    }
}