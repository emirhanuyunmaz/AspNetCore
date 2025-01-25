using MeetingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers
{
    public class HomeController : Controller{
        
        public IActionResult Index(){
            int saat = DateTime.Now.Hour;
            // int saat= 13;
            // string mesaj = saat > 12 ? "İyi Günler" : "Günaydın";
            ViewBag.Selamlama = saat > 12 ? "İyi Günler" : "Günaydın";

            var meetingInfo = new MeetingInfo()
            {
                Id=1,
                Date=new DateTime(2025,01,20,20,0,0),
                NumberOfPeople=100,
                Location="Kahramanmaraş Kongre Merkezi"

            };
            
            return View(meetingInfo);
        }
    }
}