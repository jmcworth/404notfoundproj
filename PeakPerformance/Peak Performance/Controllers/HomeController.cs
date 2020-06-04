using Microsoft.AspNet.Identity;
using Peak_Performance.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Peak_Performance.DAL;
using System.Threading.Tasks;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;

namespace Peak_Performance.Controllers
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }

        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }

        [Required]
        public string Message { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly PeakPerformanceContext db = new PeakPerformanceContext();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "We're Here to Help, Contact Us";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Recieved()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string name, string cutomerEmail, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("peakperformance1189@gmail.com", name);
                    var receiverEmail = new MailAddress("peakperformancewou@gmail.com");
                    var gmail = System.Web.Configuration.WebConfigurationManager.AppSettings["emailcontactkey"];
                    var body = "Customer Email: " + cutomerEmail + System.Environment.NewLine + System.Environment.NewLine + message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, gmail)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View("Recieved");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View("Contact");
        }

        //This is stricly for testing the example test so we can see how things work.
        public string Capitolize(string sentence)
        {
            return char.ToUpper(sentence[0]) + sentence.Substring(1);
        }

        public ActionResult TermsAndConditions()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult Credits()
        {
            return View();
        }
    }
}