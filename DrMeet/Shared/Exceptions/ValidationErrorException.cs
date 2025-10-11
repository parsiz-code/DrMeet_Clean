namespace DrMeet.Api.Shared.Exceptions;

public class ValidationErrorException(string[] errors) : Exception(string.Join('|', errors.ToArray()));

