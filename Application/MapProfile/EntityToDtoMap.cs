using Application.CartageErrands;
using Application.CartageOffers;
using Application.Users;
using AutoMapper;
using Domain.CartageErrand;
using Domain.CartageOffer;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MapProfile
{
    internal class EntityToDtoMap : Profile
    {
        public EntityToDtoMap() 
        {
            CreateMap<User, UserDto>();
            CreateMap<CartageErrand, CartageErrandDto>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.ExecutionStatus == CartageErrandExecutionStatus.Active));
            CreateMap<CartageErrand, CartageErrandWithOffersDto>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.ExecutionStatus == CartageErrandExecutionStatus.Active))
                .ForMember(dest => dest.Offers, opt => opt.MapFrom(src => src.GetSubmittedCartageOffers()));
            CreateMap<CartageOffer, CartageOfferDto>()
                .ForMember(dest => dest.HasBeenConsidered, opt => opt.MapFrom(src => src.ConsiderationStatus != CartageOfferConsiderationStatus.Waiting))
                .ForMember(dest => dest.HasBeenAccepted, opt => opt.MapFrom(src => src.ConsiderationStatus == CartageOfferConsiderationStatus.Accepted));
        }
    }
}
