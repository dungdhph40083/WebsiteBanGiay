namespace Application.Data.Enums
{
    public enum ProductStatus
    {
        Unknown = 0,
        Pending = 1,
        Sold = 2,
        Refunded = 3,
        Bepis = 4
    }

    public enum VisibilityStatus
    {
        Locked = 0,
        Available = 1
    }

    public class DefaultValues
    {
        public const string UserRoleGUID = "1bfa7246-60e1-4d82-a469-cdecf867fd01";
    }
}
