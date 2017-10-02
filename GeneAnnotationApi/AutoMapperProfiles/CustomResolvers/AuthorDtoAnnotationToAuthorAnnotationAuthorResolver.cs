using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles.CustomResolvers
{
    public class AuthorDtoAnnotationToAuthorAnnotationAuthorResolver:
        IValueResolver<AuthorDto, Author, ICollection<AnnotationAuthor>>
    {
        private readonly GeneAnnotationDBContext _context;

        public AuthorDtoAnnotationToAuthorAnnotationAuthorResolver(GeneAnnotationDBContext context)
        {
            _context = context;
        }

        public ICollection<AnnotationAuthor> Resolve(AuthorDto source, Author destination, ICollection<AnnotationAuthor> destMember, ResolutionContext context)
        {
            return source.Annotations?.Select(annotationDto => new AnnotationAuthor
                {
                    Annotation = context.Mapper.Map<Annotation>(annotationDto),
                    Author = destination
                })
                .ToList();
        }
    }
}