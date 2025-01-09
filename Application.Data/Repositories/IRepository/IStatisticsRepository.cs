using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IStatisticsRepository
    {
        Task<int> CountOrdersByStatusesAndDateAsync(byte[] statuses, DateTime startDate, DateTime endDate);
        Task<long?> GetTotalAmountByStatusesAndDateAsync(byte[] statuses, DateTime startDate, DateTime endDate);
        Task<int> GetTotalProductsAsync();
        Task<int> GetTotalCategoriesAsync();
        Task<int> GetTotalUsersAsync();
        Task<int> GetBannedAccountsAsync();
        Task<int> GetTotalVouchersAsync();
        Task<int> GetTotalContactsAsync();
    }
}
