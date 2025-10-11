using DrMeet.Api.Shared.Domian;

namespace DrMeet.Api.Shared.Extensions;

public static class DateTimeExtention
{
    public static DateTime GetNextDateForWeekDay(this WeekDay targetDay,DateTime dt)
    {
       
        int daysUntilTarget = ((int)targetDay - (int)dt.DayOfWeek + 7) % 7;
        return dt.AddDays(daysUntilTarget);
    }
    public static DateTime GetDateByDayOfWeek(this WeekDay targetDay, int weeksAhead = 0)
    {
        DateTime today = DateTime.Today;

        // تبدیل DayOfWeek به WeekDay (با فرض اینکه Saturday = 0)
        int currentDay = ((int)today.DayOfWeek + 1) % 7;
        int targetDayInt = (int)targetDay;

        int daysUntilTarget = ((targetDayInt - currentDay + 7) % 7) + (weeksAhead * 7);

        return today.AddDays(daysUntilTarget);
    }
    public static bool CompareDate( DateTime firstDate, DateTime secondDate)
    {

        return (firstDate.Year == secondDate.Year &&
                   firstDate.Month == secondDate.Month &&
                   firstDate.Day == secondDate.Day);

    }
}
