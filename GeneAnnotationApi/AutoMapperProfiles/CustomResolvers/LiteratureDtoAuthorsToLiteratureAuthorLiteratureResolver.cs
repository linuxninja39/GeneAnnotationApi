using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles.CustomResolvers
{
    public class LiteratureDtoAuthorToLiteratureAuthorLiteratureResolver:
        IValueResolver<LiteratureDto, Literature, ICollection<AuthorLiterature>>
    {
        private GeneAnnotationDBContext _context;

        public LiteratureDtoAuthorToLiteratureAuthorLiteratureResolver(GeneAnnotationDBContext context)
        {
            _context = context;
        }

        public ICollection<AuthorLiterature> Resolve(LiteratureDto source, Literature destination, ICollection<AuthorLiterature> destMember, ResolutionContext context)
        {
            return source.Annotations?.Select(authorDto => new AuthorLiterature
                {
                    Author = context.Mapper.Map<Author>(authorDto),
                    Literature = destination
                })
                .ToList();
        }
    }
}