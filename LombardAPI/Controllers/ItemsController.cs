using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public async Task UpdateItem([FromBody]ItemDto itemDto)
        {
            await _itemsService.UpdateItemAsync(_mapper.Map<Item>(itemDto));
        }

        [HttpPost]
        public async Task CreateItem([FromBody] ItemDto itemDto)
        {
            await _itemsService.CreateNewItemAsync(_mapper.Map<Item>(itemDto));
        }

        [HttpDelete("{id}")]
        public async Task DeleteItem([FromQuery] int itemId)
        {
            await _itemsService.DeleteItemAsync(itemId);
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAllItems()
        {
            var items = await _itemsService.GetAllItemsAsync();

            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }

        [HttpGet("{id}")]
        public async Task<ItemDto> GetItemById(int id)
        {
            var item = await _itemsService.GetItemByIdAsync(id);

            return _mapper.Map<ItemDto>(item);
        }

    }
}