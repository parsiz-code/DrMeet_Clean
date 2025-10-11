using DrMeet.Api.Shared.Domian;
using Humanizer;

namespace DrMeet.Api.Shared.Helpers;
public class FileUploadManager
{
   static string GetImageFormat(byte[] bytes)
    {
        // PNG: 89 50 4E 47
        if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
            return "png";

        // JPG: FF D8
        if (bytes[0] == 0xFF && bytes[1] == 0xD8)
            return "jpg";

        // GIF: 47 49 46
        if (bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46)
            return "gif";

        // WEBP: RIFF....WEBP
        if (bytes.Length > 12 &&
            bytes[0] == 0x52 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x46 &&
            bytes[8] == 0x57 && bytes[9] == 0x45 && bytes[10] == 0x42 && bytes[11] == 0x50)
            return "webp";

        return "unknown";
    }

    public static async Task<string> UploadAsync(string file, FolderImagesType type)
    {
        if (file == null)
            return string.Empty;
        //byte[] imageBytes = Convert.FromBase64String(file);

        //// تعیین نوع فایل به صورت دستی (مثلاً png)
        //string fileExtension = GetImageFormat(imageBytes); // یا jpg، webp، بسته به نوع تصویر

        //// مسیر ذخیره فایل
        //string _path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads\\" + type.ToString() + "\\");
        //string filename = Guid.NewGuid().ToString() + "." + fileExtension;

        //// ذخیره فایل
        //File.WriteAllBytes(_path + filename, imageBytes);


        //    return _path + filename;

        return file;
    }
    //public static async Task<string> UploadAsync(IFormFile file, FolderImagesType type)
    //{
    //    if (file == null)
    //        return string.Empty;
    //    string _path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads\\" + type.ToString() + "\\");
    //    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file?.FileName);
    //    if (!Directory.Exists(_path))
    //        Directory.CreateDirectory(_path);

    //    var filePath = Path.Combine(_path, filename);
    //    await using var strem = File.Create(filePath);
    //    await file.CopyToAsync(strem);

    //    return _path + filename;
    //}
    public static async Task<bool> DeleteAsync(string path)
    {

        if (File.Exists(path))
        {
            File.Delete(path);
            return true;
        }

        return false;
    }

    public bool ValidationSizeFile(IFormFile file)
    {
        if (file != null && file.Length > 204800)
        {
            return false;
        }
        return true;
    }

}
