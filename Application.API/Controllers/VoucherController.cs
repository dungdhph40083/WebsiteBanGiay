﻿using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucher VoucherRepo;
        public VoucherController(IVoucher VoucherRepo)
        {
            this.VoucherRepo = VoucherRepo;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<Voucher>>> Get()
        {
            return await VoucherRepo.GetVouchers();
        }

        [HttpGet("{ID}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<Voucher?>> Get(Guid ID)
        {
            return await VoucherRepo.GetVoucherByID(ID);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Voucher>> Post([FromBody] VoucherDTO NewVoucher)
        {
            var Response = await VoucherRepo.CreateVoucher(NewVoucher);
            return CreatedAtAction(nameof(Get), new { ID = Response.VoucherID }, Response);
        }

        [HttpPut("{ID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Voucher?>> Put(Guid ID, [FromBody] VoucherDTO UpdatedVoucher)
        {
            var Response = await VoucherRepo.UpdateVoucher(ID, UpdatedVoucher);
            if (Response == null) return NotFound();
            else return Response;
        }

        [HttpDelete("{ID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid ID) 
        {
            await VoucherRepo.DeleteVoucher(ID);
            return NoContent();
        }
    }
}
