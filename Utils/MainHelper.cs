namespace AspnetCoreMvcFull.Utils
{
  public class MainHelper
  {
    public static HttpContext _httpContext => new HttpContextAccessor().HttpContext;
    public static IWebHostEnvironment _webHostEnvironment => (IWebHostEnvironment)_httpContext.RequestServices.GetService(typeof(IWebHostEnvironment));
    public static IConfiguration _configuration => (IConfiguration)_httpContext.RequestServices.GetService(typeof(IConfiguration));

  }
}
