using AutoMapper;
using LibraryManagmentAPI.Data;
using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;
using LibraryManagmentAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowsController : ControllerBase
    {
        private readonly IBorrowsRepository _borrowsRepository;

        public BorrowsController(IBorrowsRepository borrowsRepository)
        {
            _borrowsRepository = borrowsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBorrows()
        {
            var mappedBorrowDtos = await _borrowsRepository.GetAllBorrows();

            return Ok(mappedBorrowDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddBorrow(AddBorrowDto addBorrowDto)
        {
            var borrowDto = await _borrowsRepository.AddBorrow(addBorrowDto);

            return CreatedAtAction(nameof(GetAllBorrows), new { id = borrowDto.Id }, borrowDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ReturnBorrow(Guid id)
        {
            await _borrowsRepository.DeleteBorrow(id);

            return NoContent();
        }
    }
}
