namespace Levi9Challenge.DTOs
{
    public class ReservationCreateDto
    {
        public string StudentId { get; set; }
        public string CanteenId { get; set; }
        public string Date { get; set; }      
        public string Time { get; set; }    
        public int Duration { get; set; }
    }
}
