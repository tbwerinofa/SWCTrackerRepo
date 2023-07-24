﻿namespace SWCTracker.Models
{
    public class DropDownListItem
    {
        public int Value { get; set; }
        public string? Text { get; set; }
        public string? Value2 { get; set; }
        public int Value3 { get; set; }
        public bool Selected { get; set; }
        public int Value4 { get; set; }
        public bool HasAdmin { get; set; }
        public bool IsOwner { get; set; }
        public bool IsRequired { get; set; }
        public bool IsChangeValue { get; set; }
    }
}