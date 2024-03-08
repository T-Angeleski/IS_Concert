using Microsoft.AspNetCore.Identity;

namespace ConcertLab.Web.Models {
    public class ConcertUser : IdentityUser {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Address { get; set; }
    }
}
