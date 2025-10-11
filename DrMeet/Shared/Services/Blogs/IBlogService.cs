using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Blogs.Comment.DTOs;
using DrMeet.Api.Features.Blogs.DTOs;
using DrMeet.Api.Features.Doctors.EndPoints.DTOs;

using DrMeet.Api.Shared.Domian;

using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.UserService;
using DrMeet.Domain.Blogs;
using ExtentionLibrary.Strings;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Shared.Services.Blogs;

public interface IBlogService : IDisposable
{
    #region comment
    Task<ReturnUiResult> CreateCommentBlogAsync(CreateCommentBlogDto dto, int Id, UserType userType);
    Task<ReturnUiResult> UpdateCommentBlogAsync(UpdateCommentBlogDto dto, int Id, UserType userType);
    Task<ReturnUiResult> DeleteCommentBlogAsync(DeleteBlogCommentDto dto, int Id);
    Task<ReturnUiResult> UpdateCommentStatusBlogAsync(UpdateStatusCommentBlogDto dto, int Id);
    #endregion
    #region Blog



    Task<ReturnUiResult> CreateBlogAsync(CreateBlogRequestDto dto, int DoctorId);
    Task<ReturnUiResult> CreateBlogCenterAsync(CreateBlogRequestDto dto, int CenterId);

    Task<ReturnUiResult> EditBlog(UpdateBlogRequestDto dto, int UserId);
    Task<ReturnUiResult> DeleteBlog(int BlogId, int UserId);
    Task<GetBlogDetailDto> GetBlog(int BlogId);

    Task<PagedList<GetBlogListDto>> GetBlogs(GetBlogRequestResponseParams request);
    Task<PagedList<GetBlogListDto>> GetBlogs(GetBlogByDoctorIdRequestResponseParams request);
    Task<PagedList<GetBlogListDto>> GetBlogs(GetBlogByCenterIdRequestResponseParams request);

    #endregion
}
public class BlogService : IBlogService
{

    #region Constructor

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public BlogService(IUnitOfWork unitOfWork, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    #endregion

    #region Comment

    public async Task<ReturnUiResult> CreateCommentBlogAsync(CreateCommentBlogDto dto, int Id, UserType userType)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {


            var blog = await _unitOfWork.Blog.GetByIdAsync(dto.BlogId);

            if (blog == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مقاله یافت نشد");
                return returnUiResult;
            }
            var userInfo = await _userService.GetInfo(userType, Id);

            if (userInfo == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دسترسی ندارید");
                return returnUiResult;
            }
            blog.Comments.Add(new Domain.Blogs.BlogComment
            {
           BlogId = dto.BlogId,
                Email = userInfo.Email,
                Name = userInfo.Name,
                Subject = dto.Subject,
                Text = dto.Text,
                Score = dto.Score,
                Status = true,
                UserId = userInfo.UserId
            });

            await _unitOfWork.Blog.UpdateAsync(blog);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت نظر با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت نظر با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> UpdateCommentBlogAsync(UpdateCommentBlogDto dto, int Id, UserType userType)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {


            var blog = await _unitOfWork.Blog.GetByIdAsync(dto.BlogId);

            if (blog == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مقاله یافت نشد");
                return returnUiResult;
            }


            var userInfo = await _userService.GetInfo(userType, Id);


            var comment = blog.Comments.Where(_ => _.Id.Equals(dto.Id)).FirstOrDefault();
            if (comment == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("نظر یافت نشد");
                return returnUiResult;
            }
            if (userInfo == null || comment.UserId != userInfo.UserId)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دسترسی ندارید");
                return returnUiResult;
            }


            comment.Email = userInfo.Email;
            comment.Name = userInfo.Name;
            comment.Subject = dto.Subject;
            comment.Text = dto.Text;
            comment.Score = dto.Score;
            comment.UserId = userInfo.UserId;


            await _unitOfWork.Blog.UpdateAsync(blog);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت نظر با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت نظر با خطا مواجه شد");
            return returnUiResult;
        }
    }

    public async Task<ReturnUiResult> DeleteCommentBlogAsync(DeleteBlogCommentDto dto, int CenterId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(CenterId);
            if (doctor == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دکتر یافت نشد");
                return returnUiResult;
            }

            var blog = await _unitOfWork.Blog.GetByIdAsync(dto.BlogId);

            if (blog == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مقاله یافت نشد");
                return returnUiResult;
            }
            if (!blog.CenterId.Equals(CenterId))
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                return returnUiResult;
            }

            var comment = blog.Comments.Where(_ => _.Id.Equals(dto.CommentId)).FirstOrDefault();
            if (comment == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("نظر یافت نشد");
                return returnUiResult;
            }

            blog.Comments.Remove(comment);
            await _unitOfWork.Blog.UpdateAsync(blog);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("حذف نظر با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت نظر با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> UpdateCommentStatusBlogAsync(UpdateStatusCommentBlogDto dto, int CenterId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var doctor = await _unitOfWork.Centers.GetByIdAsync(CenterId);
            if (doctor == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دکتر یافت نشد");
                return returnUiResult;
            }

            var blog = await _unitOfWork.Blog.GetByIdAsync(dto.BlogId);

            if (blog == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مقاله یافت نشد");
                return returnUiResult;
            }
            if (!blog.CenterId.Equals(CenterId))
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                return returnUiResult;
            }

            var comment = blog.Comments.Where(_ => _.Id.Equals(dto.CommentId)).FirstOrDefault();
            if (comment == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("نظر یافت نشد");
                return returnUiResult;
            }

            comment.Status = dto.Status;



            await _unitOfWork.Blog.UpdateAsync(blog);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت نظر با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت نظر با خطا مواجه شد");
            return returnUiResult;
        }
    }

    #endregion

    #region Blog

    public async Task<ReturnUiResult> CreateBlogAsync(CreateBlogRequestDto dto, int CenterId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var doctor = await _unitOfWork.Centers.GetByIdAsync(CenterId);
            if (doctor == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دکتر یافت نشد");
                return returnUiResult;
            }
            var _imagePath = await FileUploadManager.UploadAsync(dto.ImagePath, FolderImagesType.Blogs);
            Blog blog = new Blog()
            {
                SummaryText = dto.SummaryText,

                CreateDate = DateTime.Now,
                Title = dto.Title,
                Status = true,
                CenterId = CenterId,
                ImagePath = _imagePath,
                Tags = string.Join(",", dto.Tags),
                Text = dto.Text,
            };

            await _unitOfWork.Blog.AddAsync(blog);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت مقاله با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت مقاله با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> CreateBlogCenterAsync(CreateBlogRequestDto dto, int UserId )
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
          
            var _imagePath = await FileUploadManager.UploadAsync(dto.ImagePath, FolderImagesType.Blogs);
            Blog blog = new Blog()
            {
                SummaryText = dto.SummaryText,
                CreateDate = DateTime.Now,
                Title = dto.Title,
                Status = true,
                CenterId = UserId,
                ImagePath = _imagePath,
                Tags = string.Join(",", dto.Tags),
                Text = dto.Text,
            
            };

            await _unitOfWork.Blog.AddAsync(blog);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت مقاله با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت مقاله با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> EditBlog(UpdateBlogRequestDto dto, int CenterId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var blog = await _unitOfWork.Blog.GetByIdAsync(dto.Id);

            if (blog == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مقاله یافت نشد");
                return returnUiResult;
            }


            if (!blog.CenterId.Equals(CenterId))
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                return returnUiResult;
            }


            blog.CreateDate = blog.CreateDate;
            blog.Title = dto.Title;
            blog.Tags = string.Join(",", dto.Tags);
            blog.CenterId = CenterId;
            blog.Text = dto.Text;
            blog.SummaryText = dto.SummaryText;


            if (dto.ImagePath is not null)
            {
                blog.ImagePath = await FileUploadManager.UploadAsync(dto.ImagePath, FolderImagesType.Sliders);
                blog.ImagePath = blog.ImagePath;
            }
            await _unitOfWork.Blog.UpdateAsync(blog);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت مقاله با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت مقاله با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> DeleteBlog(int BlogId, int CenterId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var Blog = await _unitOfWork
                .Blog
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == BlogId);

            if (Blog == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مقاله یافت نشد");
                return returnUiResult;
            }

            if (!Blog.CenterId.Equals(CenterId))
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                return returnUiResult;
            }
            await FileUploadManager.DeleteAsync(Blog.ImagePath);
            await _unitOfWork.Blog.DeleteAsync(BlogId);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("حذف مقاله با موفقیت انجام  شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("خطا  رخ داد");
            return returnUiResult;
        }
    }

    public async Task<GetBlogDetailDto> GetBlog(int BlogId)
    {
        try
        {
            var blog = await _unitOfWork
                .Blog
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == BlogId)
               .FirstOrDefaultAsync();

            string realTime = blog.Text.CalculateReadTime();
            var newModel = new GetBlogDetailDto
            {
                SummaryText = blog.SummaryText,
                CreateDate = blog.CreateDate,
                Title = blog.Title,
                Author = new AuthorDto { Name = blog.Center.CenterUser.FirstOrDefault().User.FullName??string.Empty, ImagePath = blog.Center.CenterUser.FirstOrDefault().User.Picture??string.Empty, CountOfBlog = await _unitOfWork.Blog.AsQueryable().CountAsync() },
                Tags = string.Join(",", blog.Tags),
                Text = blog.Text,
                ImagePath = blog.ImagePath,
                Id = blog.Id,
                ReadTime = realTime,
                Comment = blog.Comments.Where(_ => _.Status).Select(_ => new BlogCommentDto
                {
                    Email = _.Email,
                    Id = _.Id,
                    Name = _.Name,
                    Score = _.Score,
                    Status = _.Status,
                    Subject = _.Subject,
                    Text = _.Text,

                }).ToList()
            };


            if (blog == null)
                return null;

            return newModel;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<PagedList<GetBlogListDto>> GetBlogs(GetBlogRequestResponseParams request)
    {
        try
        {


            var response = new PagedList<GetBlogListDto>();

            var BlogDtos = _unitOfWork
                 .Blog
                 .AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
                BlogDtos = BlogDtos.Where(_ => _.Title.Contains(request.Title));

            //.AsNoTraking()

            if (BlogDtos.ToList() is null)
                return null;

            var totalBlogCount = await BlogDtos.CountAsync();
            if (totalBlogCount == 0)
                return null;
            var result = await BlogDtos.ToPagedList<Blog, GetBlogListDto>(s => new GetBlogListDto
            {
                //CreateDate = s.CreateDate.Value,
                Title = s.Title,
                ImagePath = s.ImagePath,
                Author = new AuthorDto { Name = s.Center.CenterUser.FirstOrDefault().User.FullName??string.Empty, ImagePath = s.Center.CenterUser.FirstOrDefault().User.Picture??string.Empty, CountOfBlog = totalBlogCount },
                Tags = s.Tags,
                Text = s.Text,
                SummaryText = s.SummaryText,
                Id = s.Id
            }, request.PageNumber, request.PageSize);

            return result;

        }
        catch (Exception)
        {
            // ثبت لاگ
            return null;
        }
    }

    public async Task<PagedList<GetBlogListDto>> GetBlogs(GetBlogByDoctorIdRequestResponseParams request)
    {
        try
        {
            var response = new PagedList<GetBlogListDto>();

            var BlogDtos = _unitOfWork
                 .Blog
                 .AsQueryable();

            if ((request.DoctorId)!=0)
            {
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.DoctorId);
                if (doctor != null)
                    BlogDtos = BlogDtos.Where(_ => _.CenterId.Equals(doctor.UserId));
            }



            var result = await BlogDtos.ToPagedList<Blog, GetBlogListDto>(s => new GetBlogListDto
            {
                SummaryText = s.SummaryText,
                //CreateDate = s.CreateDate.Value,
                Title = s.Title,
                ImagePath = s.ImagePath,
                Author = new AuthorDto { Name = s.Center.CenterUser.FirstOrDefault().User.FullName??string.Empty, ImagePath = s.Center.CenterUser.FirstOrDefault().User.Picture.OrEmpty(), CountOfBlog = BlogDtos.Any() ? BlogDtos.Count() : 0 },
                Tags = s.Tags,
                Text = s.Text,
                Id = s.Id
            }, request.PageNumber, request.PageSize);

            return result;
        }
        catch (Exception)
        {
            // ثبت لاگ
            return null;
        }
    }

    public async Task<PagedList<GetBlogListDto>> GetBlogs(GetBlogByCenterIdRequestResponseParams request)
    {
        try
        {
            var response = new PagedList<GetBlogListDto>();

            var BlogDtos = _unitOfWork
                 .Blog
                 .AsQueryable();

            if (request.CenterId!=0)
            {
                var center = await _unitOfWork.Centers.GetByIdAsync(request.CenterId);
                //if (center != null)
                //    BlogDtos = BlogDtos.Where(_ => _.UserId.Equals(center.us));
            }



            var result = await BlogDtos.ToPagedList<Blog, GetBlogListDto>(s => new GetBlogListDto
            {
                SummaryText = s.SummaryText,
                //CreateDate = s.CreateDate.Value,
                Title = s.Title,
                ImagePath = s.ImagePath,
                Author = new AuthorDto { Name = s.Center.CenterUser.FirstOrDefault().User.FullName, ImagePath = s.Center.CenterUser.FirstOrDefault().User.Picture, CountOfBlog = BlogDtos.Any() ? BlogDtos.Count() : 0 },
                Tags = s.Tags,
                Text = s.Text,
                Id = s.Id
            }, request.PageNumber, request.PageSize);

            return result;
        }
        catch (Exception)
        {
            // ثبت لاگ
            return null;
        }
    }

    private void SuccessBlog(in ReturnUiResult returnUiResult)
    {
        returnUiResult.ReturnResult = ReturnResult.Success;
        returnUiResult.LstMessage.Add("ثبت مقاله با موفقیت انجام شد");
    }
    private void ErrorBlog(in ReturnUiResult returnUiResult)
    {
        returnUiResult.ReturnResult = ReturnResult.Error;
        returnUiResult.LstMessage.Add("ثبت مقاله با خطا مواجه شد");
    }
    #endregion

    #region Dispose 

    void IDisposable.Dispose()
    {
        if (_unitOfWork != null)
            _unitOfWork.Blog.TryDisposeSafe();
    }


    #endregion

}
