using System.Collections.Generic;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles.CustomResolvers
{
    public class OriginDtoToGeneOriginTypeResolver: IValueResolver<GeneDto, Gene, ICollection<GeneOriginType>>
    {
        private GeneAnnotationDBContext _context;

        public OriginDtoToGeneOriginTypeResolver(GeneAnnotationDBContext context)
        {
            _context = context;
        }

        public ICollection<GeneOriginType> Resolve(GeneDto source, Gene destination, ICollection<GeneOriginType> destMember, ResolutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}