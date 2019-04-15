using AutoMapper;
using Lombard.BL.Models;
using LombardAPI.Dtos;

namespace LombardAPI.Profiles
{
    public class ItemsProfile : Profile
    {
        public ItemsProfile()
        {
            CreateMap<ItemDto, Item>();
            CreateMap<Item, ItemDto>();
        }
    }
}
