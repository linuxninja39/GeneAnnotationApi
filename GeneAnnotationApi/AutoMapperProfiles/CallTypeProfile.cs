﻿using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class CallTypeProfile : Profile
    {
        public CallTypeProfile()
        {
            CreateMap<CallType, CallTypeDto>();
        }
    }
}