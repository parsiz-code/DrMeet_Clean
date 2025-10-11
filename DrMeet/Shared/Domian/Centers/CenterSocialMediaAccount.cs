using DrMeet.Domain.Base;
using DrMeet.Domain.Enums;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل حساب شبکه اجتماعی مرکز درمانی.
/// این کلاس اطلاعات مربوط به حساب‌های رسمی مراکز درمانی در پلتفرم‌های مختلف مانند اینستاگرام، لینکدین، توییتر و غیره را نگهداری می‌کند.
/// </summary>
public class CenterSocialMediaAccount : BaseEntityIdentity
{
 

    /// <summary>
    /// شناسه مرکز درمانی مرتبط با حساب.
    /// </summary>
    public int CenterId { get; set; }

    /// <summary>
    /// نوع پلتفرم شبکه اجتماعی (مثلاً Instagram، LinkedIn، Twitter).
    /// </summary>
    public SocialMediaPlatform Platform { get; set; }

    /// <summary>
    /// نام کاربری یا آدرس URL حساب شبکه اجتماعی (اختیاری).
    /// </summary>
    public string? UsernameOrUrl { get; set; }

    /// <summary>
    /// مرکز درمانی مرتبط با حساب.
    /// </summary>
    public Center? Center { get; set; }
}
