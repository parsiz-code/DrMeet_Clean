namespace DrMeet.Api.Shared.Exceptions;

public class UnAuthorizedException(string[] errors) : Exception(string.Join('|', errors.ToArray()));

