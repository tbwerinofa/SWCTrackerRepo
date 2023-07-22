namespace SWCTracker.Models
{
    public class DashboardItem
    {
        public int FinYear { get; set; }
        public int Id { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }

        public int Ordinal { get; set; }

        public int Count { get; set; }

        public string Type { get; set; }

        public decimal Score { get; set; }

        public DateTime DateTimeStamp { get; set; }

        public string Message { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public string FinYearMonth { get; set; }

        public string DateTimeStampString { get; set; }
    }
}
