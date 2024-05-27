using BigonWebShoppingApp.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BigonWebShoppingApp.Models
{
    public class Subscriber
    {
        public string Email { get; set; }
        public bool IsAccept { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
    }
}
