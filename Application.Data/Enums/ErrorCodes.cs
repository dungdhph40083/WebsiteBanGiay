namespace Application.Data.Enums
{
    public class ErrorResult
    {
        public const string IMAGE_TOO_BIG_ERROR = "IMAGE_TOO_BIG_ERROR";
        public const string IMAGE_IS_BROKEN_ERROR = "IMAGE_IS_BROKEN_ERROR";
    }

    public class SuccessResult
    {
        public const string IMAGE_OK                  = "IMAGE_OK";
        public const string VOUCHER_APPLIANCE_SUCCESS = "VOUCHER_APPLIANCE_SUCCESS";
        public const string VOUCHER_DISCARDED_SUCCESS = "VOUCHER_DISCARDED_SUCCESS";
        public const string VOUCHER_ALREADY_DISCARDED = "VOUCHER_ALREADY_DISCARDED";
    }

    public class ValidateErrorResult
    {
        public const string USERNAME_TAKEN              = "USERNAME_TAKEN";
        public const string USERNAME_TOO_LONG           = "USERNAME_TOO_LONG";
        public const string USERNAME_TOO_SHORT          = "USERNAME_TOO_SHORT";
        public const string PASSWORD_TOO_SHORT          = "PASSWORD_TOO_SHORT";
        public const string PASSWORD_NOT_SECURE_ENOUGH  = "PASSWORD_NOT_SECURE_ENOUGH";
        public const string BAD_EMAIL_FORMAT            = "BAD_EMAIL_FORMAT";
        public const string BAD_FORMAT                  = "BAD_FORMAT";
        public const string BAD_USERNAME                = "BAD_USERNAME";
        public const string BUT_NOBODY_CAME             = "BUT_NOBODY_CAME";
        public const string EMPTY_FIELD_NOT_ALLOWED     = "EMPTY_FIELD_NOT_ALLOWED";
        public const string OUT_OF_STOCK                = "OUT_OF_STOCK";
        public const string BAD_PHONE_NUMBER            = "BAD_PHONE_NUMBER";
        public const string BAD_INPUT                   = "BAD_INPUT";
        public const string OUT_OF_RANGE                = "OUT_OF_RANGE";
        public const string INPUT_NOT_UNIQUE            = "INPUT_NOT_UNIQUE";
        public const string WTF_HOW_DID_IT_FAIL         = "WTF_HOW_DID_IT_FAIL";

        public const string VOUCHER_EXPIRED             = "VOUCHER_EXPIRED";
        public const string VOUCHER_IS_PREMATURE        = "VOUCHER_IS_PREMATURE";
        public const string VOUCHER_RAN_OUT_OF_USES     = "VOUCHER_RAN_OUT_OF_USES";
        public const string VOUCHER_DOES_NOT_EXIST      = "VOUCHER_DOES_NOT_EXIST";
        public const string VOUCHER_REQUIREMENT_FAIL    = "VOUCHER_REQUIREMENT_FAIL";
        public const string VOUCHER_VALID               = "VOUCHER_VALID";
        public const string VOUCHER_IS_UNCHANGED        = "VOUCHER_IS_UNCHANGED";
    }
}
