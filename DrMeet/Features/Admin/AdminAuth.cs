using DrMeet.Api.Features.Account;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.UserService;
using System.Security.Claims;

namespace DrMeet.Api.Features.Admin;

public static class AdminAuth
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/AdminAuth", handler: async (
                    IUserService userService
                ) =>
            {
                return Ok("login Admin successfuly");
            })
            
            .WithTags(ApiInfo.Tag)
            .RequireAuthorization()
            .AddEndpointFilter(async (context, next) =>
            {
                ClaimsPrincipal user = context.HttpContext.User;
                var accessLevel = user.FindFirst("AccessLevel")?.Value;

                var AdminId = user.GetId(UserType.Admin);

                if (accessLevel != nameof(UserType.Admin))
                    return BadRequest("دسترسی ندارید");

                return await next(context);
            });

        }
    }

}

public static class CreateAdmin
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/CreateAdmin", handler: async (
                    IUserService userService,
               string userName
                ) =>
            {
                if (await userService.UserExists(userName))
                    return BadRequest("ادمین وجود دارد ");
                var result = await userService.CreateUser(new CreateUserRequestDto { UserName = userName, Password = "Admin", UserType = UserType.Admin });
                if (result ==0)
                    return BadRequest("ادمین ساخته نشد");
                return Ok("Create Admin Successfully");
            })
            .WithTags(ApiInfo.Tag);


        }
    }

}