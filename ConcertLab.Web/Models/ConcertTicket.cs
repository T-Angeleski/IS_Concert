namespace ConcertLab.Web.Models {
    public class ConcertTicket {
        public Guid Id { get; set; }
        public int NumberOfPeople { get; set; }

        public Guid UserId { get; set; }
        public virtual ConcertUser User { get; set; }

        public Guid ConcertId { get; set; }
        public virtual Concert Concert { get; set; }
    }
}
