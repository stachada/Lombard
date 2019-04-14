using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lombard.BL.Helpers;
using Lombard.BL.Models;
using Lombard.BL.Services;
using LombardAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LombardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemsService;

        private readonly IMapper _mapper;

        public ItemsController(IItemService itemsService, IMapper mapper)
        {
            _itemsService = itemsService;
            _mapper = mapper;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(int id,[FromBody]ItemDto itemDto)
        {
            itemDto.ItemId = id;
            try
            {
                await _itemsService.UpdateItemAsync(_mapper.Map<Item>(itemDto));
                return Ok(itemDto);

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateItem([FromBody] ItemDto itemDto)
        {

            if (!Enum.TryParse(itemDto.ProductCategory, out ProductCategory category))
            {
                return BadRequest("Specified category is invalid.");
            }

            try
            {
                itemDto.ItemId  = await _itemsService.CreateNewItemAsync(_mapper.Map<Item>(itemDto));
                return CreatedAtAction(nameof(GetItemById),new { id = itemDto.ItemId },itemDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            try
            {
                await _itemsService.DeleteItemAsync(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllItems()
        {
            var items = await _itemsService.GetAllItemsAsync();

            return Ok(_mapper.Map<IEnumerable<ItemDto>>(items));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemById(int id)
        {
            try
            {
                var item = await _itemsService.GetItemByIdAsync(id);

                return Ok(_mapper.Map<ItemDto>(item));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}