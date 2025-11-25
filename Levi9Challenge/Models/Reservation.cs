namespace Levi9Challenge.Models
{
    public enum ReservationState { Active, Cancelled }
    public class Reservation
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string CanteenId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Duration { get; set; }
        public ReservationState State { get; set; }
    }
}
