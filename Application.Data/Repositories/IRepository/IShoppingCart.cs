using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface IShoppingCart
    {
        Task<List<ShoppingCart>> GetShoppingCarts();
        Task<ShoppingCart?> GetShoppingCartByID(Guid TargetID);
        Task<List<ShoppingCart>> GetShoppingCartsByUserID(Guid TargetID);
        Task<ShoppingCart> CreateNew(ShoppingCartDTO NewShoppingCart);
        Task<ShoppingCart?> Update(Guid TargetID, ShoppingCartDTO UpdatedShoppingCart);
        Task<ShoppingCart?> Add2Cart(Guid UserID, Guid ProductDetailID, int? Quantity);
        Task Delete(Guid TargetID);
    }
}
