using DrMeet.Domain.Base;
using DrMeet.Domain.Others;

namespace DrMeet.Domain.Doctors;

/// <summary>
/// مدل نگهداری تخصص‌های مرتبط با پزشک.
/// این کلاس مشخص می‌کند که هر پزشک دارای چه تخصص‌هایی است و به کدام حوزه‌های درمانی مرتبط می‌شود.
/// </summary>
public class DoctorExpertise : BaseEntityIdentity
{
    /// <summary>
    /// شناسه پزشک که تخصص به آن تعلق دارد.
    /// این مقدار الزامی است و به موجودیت <see cref="Doctor"/> متصل می‌شود.
    /// </summary>
    public int DoctorId { get; set; }

    /// <summary>
    /// شناسه تخصص انتخاب‌شده برای پزشک.
    /// این مقدار الزامی است و به موجودیت <see cref="Expertise"/> متصل می‌شود.
    /// </summary>
    public int ExpertiseId { get; set; }

    /// <summary>
    /// شیء پزشک مرتبط با تخصص.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Doctor"/>.
    /// </summary>
    public virtual Doctor? Doctor { get; set; }

    /// <summary>
    /// شیء تخصص مرتبط با پزشک.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Expertise"/>.
    /// </summary>
    public virtual Expertise? Expertise { get; set; }
}
