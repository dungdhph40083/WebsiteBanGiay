using Application.Data.DTOs;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddress()
        {
            var address = await _addressRepository.GetAllAddress();
            return Ok(address);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var address = await _addressRepository.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }

            var addressDTO = new AddressDTO
            {
                AddressID = address.AddressID,
                UserID = address.UserID,
                RoleID = address.RoleID,
                Name = address.Name,
                PhoneNumber = address.PhoneNumber,
                Description = address.Description,
                DefaultAddress = address.DefaultAddress,
                Status = address.Status
            };
            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(AddressDTO addressDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var create = await _addressRepository.CreateAddress(addressDTO);
            return CreatedAtAction(nameof(GetAddressById), new { id = create.AddressID }, create);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, AddressDTO addressDTO)
        {


            if (id != addressDTO.AddressID)
            {
                return BadRequest();
            }

            var result = await _addressRepository.UpdateAddress(addressDTO);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var result = await _addressRepository.DeleteAddress(id);    
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}
