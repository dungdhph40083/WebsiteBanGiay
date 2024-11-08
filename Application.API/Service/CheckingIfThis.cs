using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Application.API.Service
{
    public class CheckingIfThis
    {
        private static readonly byte[] JPG_Signature = { 0xFF, 0xD8 };             // Mảng ký tự ở dạng nhị phân để kiểm tra các ký tự đầu của tệp.
        private static readonly byte[] PNG_Signature = { 0x89, 0x50, 0x4E, 0x47 }; // Nếu nó trùng "FF D8" cho các ảnh đuôi .jpg hoặc trùng "89 50 4E 47" cho tệp .png là ok
        public static bool IsAnImage(IFormFile ImageFile)
        {
            string PathExtension = Path.GetExtension(ImageFile.FileName);
            var FileStream = ImageFile.OpenReadStream();
            byte[] ImageBytes = new byte[FileStream.Length];
            FileStream.Read(ImageBytes, 0, ImageBytes.Length);

            if (ImageBytes.Take(2).SequenceEqual(JPG_Signature) || ImageBytes.Take(4).SequenceEqual(PNG_Signature)) // Hell yeah
            {
                return true;
            }
            return false;
        }
    }
}
