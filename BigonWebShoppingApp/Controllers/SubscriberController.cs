using BigonWebShoppingApp.DTOs;
using BigonWebShoppingApp.Helpers.Services;
using BigonWebShoppingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;

namespace BigonWebShoppingApp.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMailService _mail;
        public SubscriberController(AppDbContext context, IMailService mailService)
        {
            _context = context;
            _mail = mailService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubscriber(string email)
        {
            ResponseContent content = new ResponseContent();

            if (!EmailFormat(email))
                return BadRequest(Response("error", "email format sehvdir", content));



            var data = await _context.Subscribers.FirstOrDefaultAsync(x => x.Email == email);
            if (data != null && !data.IsAccept)
                return BadRequest(Response("warning", "Bu emaile artiq link gonderilib!", content));
            if (data != null && data.IsAccept)
                return BadRequest(Response("info", "Bu email abone olub....", content));


            Subscriber subscriber = new Subscriber()
            {
                Email = email,
                CreatedAt = DateTime.Now
            };

            string token = $"#demo-{subscriber.Email}-{subscriber.CreatedAt:yyyy-MM-dd HH:mm:ss.fff}-bigon";
            token = HttpUtility.UrlEncode(token);
            string url = $"{Request.Scheme}://{Request.Host}/subscribe-approve?token={token}";
            string body = $"<p>Hi,</p><p>To Sub <a href=\"{url}\">Click!</a></p>";


            var flag = await _mail.SendAsync(email, "salam", body);
            if (!flag)
                return BadRequest(Response("warning", "Link gonderiminde xeta bash verdi!", content));

            await _context.Subscribers.AddAsync(subscriber);
            await _context.SaveChangesAsync();
            return Ok(Response("success", $"{email}-a link gonderildi",content));
        }



        [Route("/subscribe-approve")]
        public async Task<IActionResult> SubscribeApprove(string token)
        {
            string pattern = @"#demo-(?<email>[^-]*)-(?<date>\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{3})-bigon";

            Match match = Regex.Match(token, pattern);

            if (!match.Success)
            {
                return Content("token is broken!");
            }

            string email = match.Groups["email"].Value;
            string dateStr = match.Groups["date"].Value;

            if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm:ss.fff", null, DateTimeStyles.None, out DateTime date))
            {
                return Content("token is broken!");
            }

            var subscriber = await _context.Subscribers
                .FirstOrDefaultAsync(m => m.Email.Equals(email) && m.CreatedAt == date);

            if (subscriber == null)
            {
                return Content("token is broken!");
            }

            if (!subscriber.IsAccept)
            {
                subscriber.IsAccept = true;
                subscriber.AcceptedAt = DateTime.Now;
            }
            await _context.SaveChangesAsync();


            return Content($"Success: Email: {email}\n" +
                $"Date: {date}");
        }
        public bool EmailFormat(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }
        public ResponseContent Response(string type, string message, ResponseContent contect)
        {
            contect.Message = message;
            contect.Type = type;
            return contect;
        }
    }





    //public enum TosType
    //{
    //    Success = 1,
    //    Warning,
    //    Info,
    //    Error
    //}
}
