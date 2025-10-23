using System.Text.Json.Serialization;

namespace AspnetCoreMvcFull.Models
{
  public class SmsResponse
  {

    [JsonPropertyName("res_code")]
    public string ResCode { get; set; }

    [JsonPropertyName("msg")]
    public string Msg { get; set; }

  }
}
