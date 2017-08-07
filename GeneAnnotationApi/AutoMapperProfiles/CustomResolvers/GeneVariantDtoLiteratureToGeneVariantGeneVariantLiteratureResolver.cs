using System.Collections.Generic;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles.CustomResolvers
{
    public class GeneVariantDtoLiteratureToGeneVariantGeneVariantLiteratureResolver:
        IValueResolver<GeneVariantDto, GeneVariant, ICollection<GeneVariantLiterature>>
    {
        private GeneAnnotationDBContext _context;

        public GeneVariantDtoLiteratureToGeneVariantGeneVariantLiteratureResolver(GeneAnnotationDBContext context)
        {
            _context = context;
        }

        public ICollection<GeneVariantLiterature> Resolve(GeneVariantDto source, GeneVariant destination, ICollection<GeneVariantLiterature> destMember, ResolutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}