
using DrMeet.Api.Shared.Persistence.DbContexts.EFCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DrMeet.Api.Shared.Persistence.Repositories;

public class EFCoreRepository<T> : IEFCoreRepository<T>
    where T : class
{

    private readonly DrMeetDbContext context;

    public EFCoreRepository(DrMeetDbContext _context)
    {
        context=_context ;
       
    }

    /// <summary>
    /// دسترسی به داده‌ها به‌صورت IQueryable برای اعمال فیلتر یا کوئری‌های پیچیده
    /// </summary>
    public IQueryable<T> AsQueryable() => context.Set<T>().AsQueryable();

    /// <summary>
    /// دریافت همه رکوردها از جدول مربوطه
    /// </summary>
    public async Task<List<T>> GetAllAsync() =>
        await context.Set<T>().ToListAsync();

    /// <summary>
    /// دریافت رکوردها بر اساس شرط مشخص‌شده
    /// </summary>
    public async Task<List<T>> GetFilterAsync(Expression<Func<T, bool>> predicate) =>
        await context.Set<T>().Where(predicate).ToListAsync();

    /// <summary>
    /// دریافت رکورد بر اساس کلید اصلی (فرض بر این است که کلید از نوع int است)
    /// </summary>
    public async Task<T?> GetByIdAsync(int id) =>
        await context.Set<T>().FindAsync(id);

    /// <summary>
    /// افزودن یک رکورد جدید
    /// </summary>
    public async Task AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// افزودن مجموعه‌ای از رکوردها
    /// </summary>
    public async Task AddRangeAsync(ICollection<T> entities)
    {
        await context.Set<T>().AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// به‌روزرسانی یک رکورد موجود
    /// </summary>
    public async Task UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }
    public async Task AttachAsync(T entity)
    {
        context.Set<T>().Attach(entity);
        await context.SaveChangesAsync();
    }
    /// <summary>
    /// حذف یک رکورد بر اساس کلید
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null) return;
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// حذف همه رکوردها از جدول
    /// </summary>
    public async Task DeleteAsync()
    {
        context.Set<T>().RemoveRange(context.Set<T>());
        await context.SaveChangesAsync();
    }
    public async Task DeleteRangeAsync(ICollection<T> entities)
    {
         context.Set<T>().RemoveRange(entities);
        await context.SaveChangesAsync();
    }
    /// <summary>
    /// دریافت رکورد بر اساس شرط، و در صورت نبود، ایجاد و ذخیره آن
    /// </summary>
    public async Task<T> GetOrAddAsync(Expression<Func<T, bool>> predicate, Func<T> create)
    {
        var existing = await context.Set<T>().FirstOrDefaultAsync(predicate);
        if (existing is not null)
            return existing;

        var entity = create();
        await AddAsync(entity);
        return entity;
    }



  


}
