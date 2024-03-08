using ConcertLab.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConcertLab.Web.Data {
    public class ApplicationDbContext : IdentityDbContext<ConcertUser> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        public virtual DbSet<Concert> Concerts { get; set; }
        public virtual DbSet<ConcertTicket> Tickets { get; set; }
        public virtual DbSet<ConcertUser> Users { get; set; }
    }
}
