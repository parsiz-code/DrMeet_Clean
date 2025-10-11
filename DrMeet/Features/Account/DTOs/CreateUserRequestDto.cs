namespace DrMeet.Api.Features.Account.DTOs
{
    public class CreateUserRequestDto
    {
        //public string FirstName { get; set; } = string.Empty;
        //public string LastName { get; set; } = string.Empty;
        //public string NationalCode { get; set; } = string.Empty;
        //public string Mobile { get; set; } = string.Empty;
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
