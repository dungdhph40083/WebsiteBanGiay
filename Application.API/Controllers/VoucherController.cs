﻿using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucher VoucherRepo;
        public VoucherController(IVoucher VoucherRepo)
        {
            this.VoucherRepo = VoucherRepo;
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

        [HttpPost]
        public async Task<ActionResult<Voucher>> Post([FromBody] VoucherDTO NewVoucher)
        {
            return await VoucherRepo.CreateVoucher(NewVoucher);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<Voucher?>> Put(Guid ID, [FromBody] VoucherDTO UpdatedVoucher)
        {
            return await VoucherRepo.UpdateVoucher(ID, UpdatedVoucher);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await VoucherRepo.DeleteVoucher(ID);
            return Ok();
        }
    }
}
