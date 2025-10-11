namespace DrMeet.Api.Shared.CustomAttribute
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSizeInBytes;

        public MaxFileSizeAttribute(int maxFileSizeInKilobytes)
        {
            _maxFileSizeInBytes = maxFileSizeInKilobytes * 1024;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null && file.Length > _maxFileSizeInBytes)
            {
                return new ValidationResult($"سایز فایل نباید بیشتر از {_maxFileSizeInBytes / 1024} کیلوبایت باشد.");
            }

            return ValidationResult.Success;
        }
    }

}
