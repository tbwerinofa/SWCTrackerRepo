
namespace SWCTracker.Models
{
	public class SaveResult
	{
		public int ID { get; set; }
		public bool IsSuccess { get; set; }
		public string Message { get; set; }

        public string Reference { get; set; }

        public bool IsAddress { get; set; }

        public byte[] ByteArray { get; set; }
        public int ChildId { get; set; }
    }
}
