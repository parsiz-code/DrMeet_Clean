using DrMeet.Api.Features.Account.DTOs;

namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

public class PatientLoginRequest
{
  public int Id { get; set; }
}
public class PatientCustomLoginRequest
{
    public int Id { get; set; }
}
public class LoginTokenRequest
{

    public int Id { get; set; } 
    public int  UserId { get; set; }
    public UserType  userType { get; set; }
}
