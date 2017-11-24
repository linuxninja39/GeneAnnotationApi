﻿using System.Collections.Generic;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.Repositories;

namespace GeneAnnotationApi.AutoMapperProfiles.CustomResolvers
{
    public class GeneCoordinateStartToGeneLocationDto:
        IValueResolver<GeneLocation, GeneLocationDto, int>
    {
        private readonly IGeneCoordinateRepository _geneCoordinateRepository;

        public GeneCoordinateStartToGeneLocationDto(
            IGeneCoordinateRepository geneCoordinateRepository
            )
        {
            _geneCoordinateRepository = geneCoordinateRepository;
        }

        public int Resolve(GeneLocation source, GeneLocationDto destination, int destMember,
            ResolutionContext context)
        {
            var min = _geneCoordinateRepository.FindMinByGene(source.Gene);
            return 1;
        }
    }
}