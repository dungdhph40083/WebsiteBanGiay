using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Application.MVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        // Sử dụng status mặc định là 200, 201, 202, 203
        [HttpGet("status")]
        public async Task<IActionResult> GetOrderCountByStatusAndDate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest(new { Message = "Ngày bắt đầu không được lớn hơn ngày kết thúc." });
            }

            // Các trạng thái cần đếm: 200-203 (thành công), 0 (thất bại), 5 (hoàn trả)
            byte[] successStatuses = { 200, 201, 202, 203, 103 };
            byte[] failedStatuses = { 0 ,66};
            byte[] returnStatuses = { 5 , 2 };

            // Đếm số lượng đơn hàng theo các trạng thái
            var successCount = await _statisticsRepository.CountOrdersByStatusesAndDateAsync(successStatuses, startDate, endDate);
            var failedCount = await _statisticsRepository.CountOrdersByStatusesAndDateAsync(failedStatuses, startDate, endDate);
            var returnCount = await _statisticsRepository.CountOrdersByStatusesAndDateAsync(returnStatuses, startDate, endDate);

            // Tính tổng tiền cho các trạng thái
            var successTotalAmount = await _statisticsRepository.GetTotalAmountByStatusesAndDateAsync(successStatuses, startDate, endDate);
            var failedTotalAmount = await _statisticsRepository.GetTotalAmountByStatusesAndDateAsync(failedStatuses, startDate, endDate);
            var returnTotalAmount = await _statisticsRepository.GetTotalAmountByStatusesAndDateAsync(returnStatuses, startDate, endDate);

            //
            // Các thống kê bổ sung không lọc theo thời gian
            var totalProducts = await _statisticsRepository.GetTotalProductsAsync();
            var totalCategories = await _statisticsRepository.GetTotalCategoriesAsync();
            var totalUsers = await _statisticsRepository.GetTotalUsersAsync();
            var bannedAccounts = await _statisticsRepository.GetBannedAccountsAsync();
            var totalVouchers = await _statisticsRepository.GetTotalVouchersAsync();
            var totalContacts = await _statisticsRepository.GetTotalContactsAsync();
            return Ok(new
            {
                SuccessStatuses = successStatuses,
                FailedStatuses = failedStatuses,
                ReturnStatuses = returnStatuses,
                StartDate = startDate.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss"),
                EndDate = endDate.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss"),
                ThanhCong = successCount,
                ThatBai = failedCount,
                Hoantra = returnCount,
                TotalAmountThanhCong = successTotalAmount,
                TotalAmountThatBai = failedTotalAmount,
                TotalAmountHoantra = returnTotalAmount,
                //
                TotalProducts = totalProducts,
                TotalCategories = totalCategories,
                TotalUsers = totalUsers,
                BannedAccounts = bannedAccounts,
                TotalVouchers = totalVouchers,
                TotalContacts = totalContacts
            });
        }


    }
}
