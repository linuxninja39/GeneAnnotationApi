using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles.CustomResolvers
{
    public class GeneVariantDtoAnnotationToGeneVariantAnnotationGeneVariantResolver:
        IValueResolver<GeneVariantDto, GeneVariant, ICollection<AnnotationGeneVariant>>
    {
        private readonly GeneAnnotationDBContext _context;

        public GeneVariantDtoAnnotationToGeneVariantAnnotationGeneVariantResolver(GeneAnnotationDBContext context)
        {
            _context = context;
        }

        public ICollection<AnnotationGeneVariant> Resolve(GeneVariantDto source, GeneVariant destination, ICollection<AnnotationGeneVariant> destMember, ResolutionContext context)
        {
            return source.Annotation?.Select(annotationDto => new AnnotationGeneVariant
                {
                    Annotation = context.Mapper.Map<Annotation>(annotationDto),
                    GeneVariant = destination
                })
                .ToList();
        }
    }
}