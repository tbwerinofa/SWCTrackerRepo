namespace BusinessObject
{
    using System;
    
    public partial class WageRateDetailResultSet
    {
        public string? GradingSystem { get; set; }
        public int FinYear { get; set; }
        public string? Grade { get; set; }
        public Nullable<int> Ordinal { get; set; }
        public string? SubSector { get; set; }
        public string? Occupation { get; set; }
        public string? OccupationGroup { get; set; }
        public int OccupationGroupId { get; set; }
        public int FinYearId { get; set; }
        public string? EmploymentCondition { get; set; }
        public decimal Amount { get; set; }
        public int OccupationId { get; set; }
        public decimal PropertyValue { get; set; }
        public Nullable<decimal> AveragePropertyValue { get; set; }
        public bool IsPrefix { get; set; }
        public string? MeasurementUnit { get; set; }
        public string? Symbol { get; set; }
        public decimal CPIIndex { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
