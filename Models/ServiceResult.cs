namespace AspnetCoreMvcFull.Models
{
  public class ServiceResult
  {// Indicates if the operation was successful.
    public bool IsSuccessful { get; set; } = false;

    // A user-facing message (e.g., for TempData).
    public string Message { get; set; }

    // An internal notification message (e.g., for SMS/Email alerts).
    public string NotificationMessage { get; set; }

    // (Optional) Stores the newly created/updated entity ID or other relevant data.
    public object Data { get; set; }
  }
}
