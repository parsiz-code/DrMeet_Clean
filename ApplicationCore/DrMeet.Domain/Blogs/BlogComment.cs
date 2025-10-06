using DrMeet.Domain.Others;

namespace DrMeet.Domain.Blogs;

/// <summary>
/// مدل نظر ثبت‌شده برای یک مقاله.
/// این کلاس نمایانگر بازخورد یا دیدگاه کاربران درباره‌ی یک پست وبلاگ است.
/// </summary>
public class BlogComment : Comment
{
   
    /// <summary>
    /// شناسه مقاله .
    /// </summary>
    public required int BlogId { get; set; }

    public Blog? Blog { get; set; } 
}
