namespace Web_Project.Models.Db
{
    public class Record
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PairCount { get; set; }
        public int AttemptCount { get; set; }
        public TimeSpan Time { get; set; }
    }
}
