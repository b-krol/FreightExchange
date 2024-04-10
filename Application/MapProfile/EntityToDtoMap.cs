using Application.JobOffers;
using Application.Users;
using AutoMapper;
using Domain.JobOffer;
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
            CreateMap<JobOffer, JobOfferDto>();
        }
    }
}
