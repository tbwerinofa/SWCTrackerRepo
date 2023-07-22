namespace SWCTracker.Models
{
    public static class ModelConstant
    {
        public static object FormControlInp = new { @class = "form-control input-sm" };
        public static object FormControlInpMultiple = new { @class = "form-control input-sm", @multiple = "multiple" };
        public static object LabelRequired = new { @class = "control-label col-md-4 input-sm field-required" };
        public static object LabelStandard = new { @class = "control-label col-md-4 input-sm" };
        public static object FormControlInpNumberOnly = new { @class = "form-control input-sm number-only" };
        public static object FormControlInpReadOnly = new { @class = "form-control input-sm", @readonly = "readonly" };
        public static object EditorForContactNoAttributes = new { @class = "form-control number-only  input-sm", maxlength = 10 };
        public static object FormControlInpContactNo = new { @class = "form-control input-sm number-only", minlength = 10, maxlength = 10, @placeholder = "xxxxxxxxxx" };
        public static object EditorForNumericAttributes = new { @class = "form-control number-only input-sm text-right ", maxlength = 3, min = 1, max = 200 };
    }
}
