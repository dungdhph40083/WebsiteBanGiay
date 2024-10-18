using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface IShoppingCart
    {
        Task<List<ShoppingCart>> GetShoppingCarts();
        Task<ShoppingCart?> GetShoppingCartByID(Guid TargetID);
        Task<ShoppingCart> CreateNew(ShoppingCartDTO NewShoppingCart);
        Task<ShoppingCart?> Update(Guid TargetID, ShoppingCartDTO UpdatedShoppingCart);
        Task Delete(Guid TargetID);
    }
}
