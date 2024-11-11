namespace Application.Data.Enums
{
    public class ErrorResult
    {
        public const string IMAGE_TOO_BIG_ERROR = "IMAGE_TOO_BIG_ERROR";
        public const string IMAGE_IS_BROKEN_ERROR = "IMAGE_IS_BROKEN_ERROR";
    }

    public class SuccessResult
    {
        public const string IMAGE_OK = "IMAGE_OK";
    }

    public class ValidateErrorResult
    {
        public const string USERNAME_TAKEN              = "USERNAME_TAKEN";
        public const string USERNAME_TOO_LONG           = "USERNAME_TOO_LONG";
        public const string USERNAME_TOO_SHORT          = "USERNAME_TOO_SHORT";
        public const string PASSWORD_TOO_SHORT          = "PASSWORD_TOO_SHORT";
        public const string PASSWORD_NOT_SECURE_ENOUGH  = "PASSWORD_NOT_SECURE_ENOUGH";
        public const string BAD_EMAIL_FORMAT            = "BAD_EMAIL_FORMAT";
        public const string BAD_USERNAME                = "BAD_USERNAME";
        public const string EMPTY_FIELD_NOT_ALLOWED     = "EMPTY_FIELD_NOT_ALLOWED";
        public const string OUT_OF_STOCK                = "OUT_OF_STOCK";
        public const string BAD_PHONE_NUMBER            = "BAD_PHONE_NUMBER";
        public const string BAD_INPUT                   = "BAD_INPUT";
    }
}
