using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Aspose.Pdf.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISize SizeRepo;
        public SizeController(ISize SizeRepo)
        {
            this.SizeRepo = SizeRepo;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Size>>> Get()
        {
            return await SizeRepo.GetSizes();
        }

        [HttpGet("{ID}")]
        [AllowAnonymous]
        public async Task<ActionResult<Size?>> Get(Guid ID)
        {
            return await SizeRepo.GetSizeByID(ID);
        }

        [HttpPost]
        public async Task<ActionResult<Size>> Post([FromBody] SizeDTO NewSize)
        {
            var Target = await SizeRepo.SizeNameAvailability(NewSize.Name);
            if (!Target) return Conflict();

            var Response = await SizeRepo.AddSize(NewSize);
            return CreatedAtAction(nameof(Get), new { ID = Response.SizeID }, Response);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<Size?>> Put(Guid ID, [FromBody] SizeDTO UpdatedSize)
        {
            var OldSize = await SizeRepo.GetSizeByID(ID);
            if (string.Equals(OldSize?.Name, UpdatedSize.Name, StringComparison.OrdinalIgnoreCase))
            {
                var Target = await SizeRepo.SizeNameAvailability(UpdatedSize.Name);
                if (!Target) return Conflict();
            }

            return await SizeRepo.UpdateSize(ID, UpdatedSize);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            try
            {
                await SizeRepo.DeleteSize(ID);
                return NoContent();
            }
            catch (Exception)
            {
                var Target = await SizeRepo.GetSizeByID(ID);
                if (Target != null) return NoContent();
                else return Conflict();
            }
        }
    }
}

/*
Check Name:
- Danh mục -- done
- Size -- done
- Màu -- done
- Tài khoản : Check Tên tài khoản , Email , SĐT -- done
- Voucher -- done
 */
