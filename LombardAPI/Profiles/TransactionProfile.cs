using AutoMapper;
using Lombard.BL.Models;
using LombardAPI.Dtos;

namespace LombardAPI.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionDto, Transaction>();
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
