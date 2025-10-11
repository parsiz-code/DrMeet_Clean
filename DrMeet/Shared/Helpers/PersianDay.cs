using DrMeet.Api.Shared.Domian;

namespace DrMeet.Api.Shared.Helpers;

public static class PersianDay
{
    public record PersianDayModel(int Id, string Name);
    public static WeekDay GetTodayWeekDay(DateTime? dt)
    {
        // گرفتن روز فعلی از DateTime
        var systemDay =dt.HasValue?dt.Value.DayOfWeek: DateTime.Now.DayOfWeek;

        // تبدیل به عدد صحیح
        int dayIndex = (int)systemDay;

        // تطبیق با WeekDay شما
        // چون WeekDay از Saturday=0 شروع می‌شه، باید جابجا کنیم
        // یعنی Sunday=1 در System باید بشه Sunday=1 در WeekDay و Saturday=6 باید بشه Saturday=0

        // تبدیل از System.DayOfWeek به WeekDay
        WeekDay today = (WeekDay)(((dayIndex + 1) % 7));

        return today;
    }


    public static IReadOnlyList<PersianDayModel> PersianDayList =>
    [
        new((int)DayOfWeek.Saturday, "شنبه"),
        new((int)DayOfWeek.Sunday, "یک‌شنبه"),
        new((int)DayOfWeek.Monday, "دوشنبه"),
        new((int)DayOfWeek.Tuesday, "سه‌شنبه"),
        new((int)DayOfWeek.Wednesday, "چهار‌شنبه"),
        new((int)DayOfWeek.Thursday, "پنج‌شنبه"),
        new((int)DayOfWeek.Friday, "جمعه")
    ];

    public static IReadOnlyList<(WeekDay Id, string Name)> PersianOfDayList =>
 [
     new(WeekDay.Saturday, "شنبه"),
        new(WeekDay.Sunday, "یک‌شنبه"),
        new(WeekDay.Monday, "دوشنبه"),
        new(WeekDay.Tuesday, "سه‌شنبه"),
        new(WeekDay.Wednesday, "چهار‌شنبه"),
        new(WeekDay.Thursday, "پنج‌شنبه"),
        new(WeekDay.Friday, "جمعه")
 ];
    public static string GetNameOfDay(WeekDay WeekDay)
    {
        return PersianOfDayList.FirstOrDefault(_ => _.Id == WeekDay)!.Name;
    }
}