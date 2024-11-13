using Application.Data.Enums;

namespace Application.API.Service
{
    public class ImageUploaderValidator
    {
        // Mảng ký tự ở dạng nhị phân để kiểm tra các ký tự đầu của tệp.
        // Nếu nó trùng "FF D8" ở đầu & "FF D9" ở cuối cho các ảnh đuôi .jpg
        // hoặc trùng "89 50 4E 47" ờ đầu & "49 45 4E 44 AE 42 60 82" cho tệp .png là ok

        private static readonly byte[] JPG_Signature_Start = [0xFF, 0xD8];                
        private static readonly byte[] JPG_Signature_End = [0xFF, 0xD9];                  
        private static readonly byte[] PNG_Signature_Start = [0x89, 0x50, 0x4E, 0x47];    
        private static readonly byte[] PNG_Signature_End = [0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82];

        public static string ValidateImageSizeAndHeader(IFormFile ImageFile, long MaxFileSize)
        {
            if (ImageFile.Length > MaxFileSize)
            {
                return ErrorResult.IMAGE_TOO_BIG_ERROR;
            }
            if (!IsAnImage(ImageFile))
            {
                return ErrorResult.IMAGE_IS_BROKEN_ERROR;
            }
            return SuccessResult.IMAGE_OK;
        }
        private static bool IsAnImage(IFormFile ImageFile)
        {
            byte[] ImageBytes = GetImageBytes(ImageFile);

            if ((ImageBytes.Take(JPG_Signature_Start.Length).SequenceEqual(JPG_Signature_Start)
                && ImageBytes.TakeLast(JPG_Signature_End.Length).SequenceEqual(JPG_Signature_End))
                || (ImageBytes.Take(PNG_Signature_Start.Length).SequenceEqual(PNG_Signature_Start)
                && ImageBytes.TakeLast(PNG_Signature_End.Length).SequenceEqual(PNG_Signature_End)))
            {
                return true;
            }
            return false;
        }

        private static byte[] GetImageBytes(IFormFile ImageFile)
        {
            string PathExtension = Path.GetExtension(ImageFile.FileName);
            var FileStream = ImageFile.OpenReadStream();
            byte[] ImageBytes = new byte[FileStream.Length];
            FileStream.Read(ImageBytes, 0, ImageBytes.Length);
            return ImageBytes;
        }

        public static string GetMIMEType(byte[] ImageBytes)
        {
            if (ImageBytes.Take(JPG_Signature_Start.Length).SequenceEqual(JPG_Signature_Start)
             && ImageBytes.TakeLast(JPG_Signature_End.Length).SequenceEqual(JPG_Signature_End))
            {
                return "image/jpeg";
            }
            if (ImageBytes.Take(PNG_Signature_Start.Length).SequenceEqual(PNG_Signature_Start)
             && ImageBytes.TakeLast(PNG_Signature_End.Length).SequenceEqual(PNG_Signature_End))
            {
                return "image/png";
            }
            return string.Empty;
        }
    }
}
