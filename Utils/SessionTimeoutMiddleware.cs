namespace AspnetCoreMvcFull.Utils
{
  public class SessionTimeoutMiddleware
  {
    private readonly RequestDelegate _next;

    public SessionTimeoutMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      //if (!context.User.Identity.IsAuthenticated)
      //{
      //  context.Response.Redirect("/Auth/Index");
      //  return;
      //}

      await _next(context);
    }
  }
}
