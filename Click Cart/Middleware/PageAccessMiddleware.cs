namespace Click_Cart.Middleware
{
    public class PageAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public PageAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check if the request is for an Admin page
            if (context.Request.Path.StartsWithSegments("/Admin"))
            {
                var userRole = context.Session.GetString("UserRole");

                // If the user is not an admin, redirect to the Access Denied page
                if (userRole != "Admin")
                {
                    context.Response.Redirect("/Common/Login/Index");
                    return;
                }
            }
            else if (context.Request.Path.StartsWithSegments("/User"))
            {
                var UserRole = context.Session.GetString("UserRole");
                if (UserRole != "User")
                {
                    context.Response.Redirect("/Common/Login/Index");
                    return;
                }
            }

            await _next(context); // Continue to the next middleware if allowed
        }
    }
}
