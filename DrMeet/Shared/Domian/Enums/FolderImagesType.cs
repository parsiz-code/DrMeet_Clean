namespace DrMeet.Domain.Enums;

public enum FolderImagesType
{
    None,
    Sliders,
    Doctor,
    Blogs,
    Author,
    Center,
    Insurance,
}
public enum ShiftActivityStatus
{
    Active = 0,
    NotActive = 1
}
[Flags]
public enum ShiftStatus
{
    None = 0,
    Open = 1,    // قابل رزرو
    Booked = 2,    // رزرو شده
    InProgress = 4,    // در حال انجام
    Completed = 8,    // انجام شده
    Cancelled = 16    // لغو شده
}
public enum ShiftType
{
    Morning = 0,
    Afternoon = 1
}