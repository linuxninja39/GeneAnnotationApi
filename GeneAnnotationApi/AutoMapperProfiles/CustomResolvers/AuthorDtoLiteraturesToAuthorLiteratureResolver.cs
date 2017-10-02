using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles.CustomResolvers
{
    public class AuthorDtoLiteraturesToAuthorLiteratureResolver:
        IValueResolver<AuthorDto, Author, ICollection<AuthorLiterature>>
    {
        private readonly GeneAnnotationDBContext _context;

        public AuthorDtoLiteraturesToAuthorLiteratureResolver(GeneAnnotationDBContext context)
        {
            _context = context;
        }

        public ICollection<AuthorLiterature> Resolve(AuthorDto source, Author destination, ICollection<AuthorLiterature> destMember, ResolutionContext context)
        {
            return source.Literatures?.Select(literatureDto => new AuthorLiterature
                {
                    Literature = context.Mapper.Map<Literature>(literatureDto),
                    Author = destination
                })
                .ToList();
        }
    }
}