namespace Note.MVCWebApp.Models
{
    public class ErrorViewModel
    {
        public int? Status { get; set; }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
