using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucher VoucherRepo;
        private readonly IShoppingCart ShoppingCartRepo;
        public VoucherController(IVoucher VoucherRepo, IShoppingCart ShoppingCartRepo)
        {
            this.VoucherRepo = VoucherRepo;
            this.ShoppingCartRepo = ShoppingCartRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Voucher>>> Get()
        {
            return await VoucherRepo.GetVouchers();
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<Voucher?>> Get(Guid ID)
        {
            return await VoucherRepo.GetVoucherByID(ID);
        }

        [HttpGet("WhatVoucherAreTheyUsing/{ID}")]
        public async Task<ActionResult<Voucher?>> GetByUser(Guid ID)
        {
            var Response = await VoucherRepo.GetVoucherByUserID(ID);
            return Response;
        }

        [HttpPost]
        public async Task<ActionResult<Voucher>> Post([FromBody] VoucherDTO NewVoucher)
        {
            var CheckVal = await VoucherRepo.GetVoucherByVoucherCode(NewVoucher.VoucherCode);
            if (CheckVal != null) return Conflict();

            var Response = await VoucherRepo.CreateVoucher(NewVoucher);
            return CreatedAtAction(nameof(Get), new { ID = Response.VoucherID }, Response);
        }

        [HttpPost("Validate/{UserID}/{VoucherCode}")]
        public async Task<ActionResult<string>> ValidateVoucher(Guid UserID, string VoucherCode)
        {
            var Response = await VoucherRepo.VoucherValidator(UserID, VoucherCode);
            if (Response != ValidateErrorResult.VOUCHER_VALID)
            {
                await ShoppingCartRepo.ApplyVoucher(UserID, null);
            }
            return Response;
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<Voucher?>> Put(Guid ID, [FromBody] VoucherDTO UpdatedVoucher)
        {
            var OldVoucher = await VoucherRepo.GetVoucherByID(ID);
            if (UpdatedVoucher.VoucherCode != OldVoucher?.VoucherCode)
            {
                var CheckVal = await VoucherRepo.GetVoucherByVoucherCode(UpdatedVoucher.VoucherCode);
                if (CheckVal != null) return Conflict();
            }

            var Response = await VoucherRepo.UpdateVoucher(ID, UpdatedVoucher);
            if (Response == null) return NotFound();
            else return Response;
        }

        [HttpPatch("ToggleStatus/{ID}")]
        public async Task<ActionResult<Voucher?>> ChangeStatus(Guid ID)
        {
            return await VoucherRepo.ToggleStatus(ID);
        }

        [HttpPatch("StopVoucher/{ID}")]
        public async Task<ActionResult<Voucher?>> StopVoucher(Guid ID)
        {
            return await VoucherRepo.StopVoucher(ID);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID) 
        {
            try
            {
                await VoucherRepo.DeleteVoucher(ID);
                return NoContent();
            }
            catch (Exception)
            {
                var Target = await VoucherRepo.GetVoucherByID(ID);
                if (Target != null) return Conflict();
                else return NoContent();
            }
        }
    }
}
