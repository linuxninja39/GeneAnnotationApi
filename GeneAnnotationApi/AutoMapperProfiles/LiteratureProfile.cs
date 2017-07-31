using System.Linq;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class LiteratureProfile : Profile
    {
        public LiteratureProfile()
        {
            CreateMap<Literature, LiteratureDto>()
                .ForMember(
                    destinationMember => destinationMember.Author,
                    opt => opt.MapFrom(
                        lit => lit.AuthorLiterature.Select(authLit => authLit.Author)
                        )
                    )
                .ForMember(
                    destinationMember => destinationMember.Annotation,
                    opt => opt.MapFrom(
                        lit => lit.AnnotationLiterature.Select(authLit => authLit .Annotation)
                        )
                    )
                ;
        }
    }
}