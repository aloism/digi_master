using System.Text.Json.Serialization;

namespace AspnetCoreMvcFull.Services.dtos
{
  public class ApiResponse
  {

    [JsonPropertyName("res_code")]
    public string ResCode { get; set; }

    [JsonPropertyName("dataId")]
    public string DataId { get; set; }

  }
}
