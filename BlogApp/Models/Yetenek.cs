using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class Yetenek
    {
        public int Id { get; set; }
        public string YetenekAdi { get; set; }
        public int Progress { get; set; }
    }
}