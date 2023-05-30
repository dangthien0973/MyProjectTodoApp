using APIToDoListV1.Reponsitories;
using APIToDoListV1.Utils;

namespace APIToDoListV1.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context,IUserRepository userRepository,IJwtUtils jwtUtils) 
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if(userId!=null)
            context.Items["User"] = userRepository.GetUserById(new Guid(userId));

            await _next(context);
        }
    }
}
