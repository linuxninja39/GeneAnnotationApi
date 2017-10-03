using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles.CustomResolvers
{
    public class LiteratureDtoAnnotationToLiteratureAnnotationLiteratureResolver:
        IValueResolver<LiteratureDto, Literature, ICollection<AnnotationLiterature>>
    {
        private GeneAnnotationDBContext _context;

        public LiteratureDtoAnnotationToLiteratureAnnotationLiteratureResolver(GeneAnnotationDBContext context)
        {
            _context = context;
        }

        public ICollection<AnnotationLiterature> Resolve(LiteratureDto source, Literature destination, ICollection<AnnotationLiterature> destMember, ResolutionContext context)
        {
            return source.Annotations?.Select(annotationDto => new AnnotationLiterature
                {
                    Annotation = context.Mapper.Map<Annotation>(annotationDto),
                    Literature = destination
                })
                .ToList();
        }
    }
}