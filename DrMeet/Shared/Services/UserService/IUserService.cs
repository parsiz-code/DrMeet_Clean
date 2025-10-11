using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Domain.Users;

namespace DrMeet.Api.Shared.Services.UserService;

public interface IUserService
{
    Task<bool> UserInCenter(int userId, int centerId);
    Task<LoginTokenRequest> LoginAllAsync(UserLoginRequestDto userLoginDto);
    Task<UserInfoCommentResponseDto> GetInfo(UserType userType, int Id);
    Task<DetailsAdminUserResponseDto> GetUserDetails(int Id);
    Task<bool> UserExists(string UserName);
    int GetPatientId();
    Task<int> GetId(UserType userType, int Id);
    Task<int> CreateUser(CreateUserRequestDto newUserDto);
    Task<(int userId, UserType userType)> LoginAsync(UserLoginRequestDto userLoginDto);
    Task<User> GetUser(int Id);
    Task<AdminLoginResponseDto> LoginAdminAsync(AdminLoginRequestDto adminDto);
}
