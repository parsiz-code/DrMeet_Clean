using DrMeet.Api.Features.Account.DTOs;

using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.Doctors;
using DrMeet.Api.Shared.Services.JwtService;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.Patients;
using DrMeet.Domain.Users;
using ExtentionLibrary.Strings;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace DrMeet.Api.Shared.Services.UserService;


public class UserService(IJwtService jwtService, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, IParsizTebApiService parsizTebApiService) : IUserService
{
    public async Task<int> CreateUser(CreateUserRequestDto newUserDto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var salt = Hashing.GenerateSalt();
            var user = new Domain.Users.User
            {
                Password = Hashing.HashPassword(newUserDto.Password, salt),
                Salt = salt,
                UserName = newUserDto.UserName,
                UserType = newUserDto.UserType,
                FullName = newUserDto.UserName,
                //NationalCode = newUserDto.NationalCode,
                //FirstName = newUserDto.FirstName,
                //LastName = newUserDto.LastName,
                //Mobile = newUserDto.Mobile,
            };
            await unitOfWork.Users.AddAsync(user);


            return user.Id;
        }
        catch (Exception)
        {

            return 0;
        }
    }

    public int GetPatientId()
    {
        var token = contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        var result = jwtService.GetClaim(token, CustomClaims.PatientId);

        if (string.IsNullOrEmpty(result))
        {
            throw new InvalidOperationException("PatientId claim is null");
        }
        if (int.TryParse(result, out int patientId))
            return patientId;
        return 0;
    }

    public async Task<AdminLoginResponseDto> LoginAdminAsync(AdminLoginRequestDto adminDto)
    {
        var admin = unitOfWork.Users.AsQueryable().FirstOrDefault(_ => _.UserName == adminDto.Username);
        if (admin == null)
            return null;
        if (admin.UserType != UserType.Admin)
            return null;
        //var user = await GetUser(admin.Id);
        if (admin == null) return null;
        var IsValidPassord = Hashing.ComparePassword(admin.Password, adminDto.Password, admin.Salt) ? new AdminLoginResponseDto() { Id = admin.Id } : null;
        return IsValidPassord;

    }
    public async Task<(int userId,UserType userType)> LoginAsync(UserLoginRequestDto userLoginDto)
    {
        var user = unitOfWork.Users.AsQueryable().FirstOrDefault(_ => _.UserName == userLoginDto.Username);
        if (user == null)
            return (0, UserType.None);
  
        var IsValidPassord = Hashing.ComparePassword(user.Password, userLoginDto.Password, user.Salt) ? user.Id : 0;
        return (IsValidPassord, user.UserType??UserType.None);
    }

    public async Task<LoginTokenRequest?> LoginAllAsync(UserLoginRequestDto userLoginDto)
    {
        var user = await unitOfWork.Users
            .AsQueryable()
            .Include(u => u.CenterUser)
            .FirstOrDefaultAsync(u => u.UserName == userLoginDto.Username);

        if (user is null)
            return null;

        var isValidPassword = Hashing.ComparePassword(user.Password, userLoginDto.Password, user.Salt);
        if (!isValidPassword)
            return null;

        var model = new LoginTokenRequest
        {
            UserId = user.Id,
            userType = user.UserType ?? UserType.None
        };

        model.Id = model.userType switch
        {
            UserType.Patient => user.Patient?.Id ?? 0,
            UserType.Center => user.CenterUser?.FirstOrDefault()?.CenterId ?? 0,
            UserType.Admin => user.Id,
            UserType.Doctor => user.Id,
            _ => user.Id
        };

        return model;
    }

    public async Task<User> GetUser(int Id)
    {
        return await unitOfWork.Users.GetByIdAsync(Id);
    }
    public async Task<DetailsAdminUserResponseDto> GetUserDetails(int Id)
    {
        var model = new DetailsAdminUserResponseDto();

        var user = await unitOfWork.Users.GetByIdAsync(Id);
        if (user is null)
            return null;

        model.PersonalInfo = new
        {
            userName = user.UserName,
            FullName = user.FullName,
        };
        return model;
    }
    public async Task<bool> UserExists(string UserName)
    {
        return await unitOfWork.Users.AsQueryable().AnyAsync(_ => _.UserName == UserName);
    }

    public async Task<int> GetId(UserType userType,int Id)
    {
        //if (userType == UserType.Doctor)
        //{
        //    return (await unitOfWork.Doctors.AsQueryable().Where(_ => _.Id == Id).Select(_=>_.UserId).FirstOrDefaultAsync());
        //}
        //else if (userType == UserType.Patient)
        //{
        //    //bool statusId = int.TryParse(Id, out var id);
        //    //if (statusId)
        //    //{
        //    //    return (await unitOfWork.Patients.AsQueryable().Where(_ => _.PatientRemoteId == id).Select(_ => _.UserId).FirstOrDefaultAsync());

        //    //}
        //    //else
        //    //{
        //    //    return (await unitOfWork.Patients.AsQueryable().Where(_ => _.Id == Id).Select(_ => _.UserId).FirstOrDefaultAsync());

        //    //}

        //}
        //else if (userType == UserType.Center)
        //{
        //    return (await unitOfWork.Centers.AsQueryable().Where(_ => _.Id == Id).Select(_ => _.UserId).FirstOrDefaultAsync());
        //}

        //else if (userType == UserType.Admin)
        //{
        //    return (await unitOfWork.Users.AsQueryable().Where(_ => _.Id == Id).Select(_ => _.Id).FirstOrDefaultAsync());
        //}
        //else
        //    return 0;

        return 0;

    }

    public async Task<bool> UserInCenter(int userId, int centerId)=>
        await unitOfWork.CenterUsers.AsQueryable().Where(_=>_.UserId==userId&&_.CenterId==centerId).AnyAsync();

    public async Task<UserInfoCommentResponseDto> GetInfo(UserType userType, int Id)
    {
   
        var model = new UserInfoCommentResponseDto();
        if (userType == UserType.Doctor)
        {
            var data = (await unitOfWork.Doctors.AsQueryable().Where(_ => _.Id == Id).FirstOrDefaultAsync());
            model.Name = data.User.FullName;
            model.Email = data.User.Email.OrEmpty();
            model.UserId = data.UserId;
        }
        else if (userType == UserType.Patient)
        {
          
                var data = (await unitOfWork.Patients.AsQueryable().Where(_ => _.PatientRemoteId == Id).FirstOrDefaultAsync());
                model.Name = data.User.FirstName + " " + data.User.LastName;
                model.Email = data.User.Email.OrEmpty();
                model.UserId = data.UserId;
            
            

        }
        else if (userType == UserType.Center)
        {
            var data = (await unitOfWork.Centers.AsQueryable().Where(_ => _.Id == Id).FirstOrDefaultAsync());
            model.Name = data.Name;
            model.Email = data.Email.OrEmpty();
            //model.UserId = data.UserId;
        }

        else if (userType == UserType.Admin)
        {
            var data = (await unitOfWork.Users.AsQueryable().Where(_ => _.Id == Id).FirstOrDefaultAsync());
            model.Name = data.FullName;
            model.Email = data.UserName.OrEmpty() + "@admin.com";
            model.UserId = data.Id;
        }
        else return null;

            return model;
    }
}