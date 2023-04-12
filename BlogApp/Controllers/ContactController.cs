using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult ThankYou()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Send(string inputName, string inputEmail, string inputSubject, string inputMessage)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("m.albiononlinex@gmail.com", "rlppjypdpeezzypc"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(inputEmail),
                Subject = inputSubject,
                Body = $"Name: {inputName}\nEmail: {inputEmail}\n\nMessage:\n{inputMessage}"
            };
            mailMessage.To.Add("m.albiononlinex@gmail.com");

            await smtpClient.SendMailAsync(mailMessage);

            return RedirectToAction("ThankYou", "Contact");
        }

    }
}