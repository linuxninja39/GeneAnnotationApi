using System.Linq;
using AutoMapper;
using GeneAnnotationApi.AutoMapperProfiles.CustomResolvers;
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
                    destinationMember => destinationMember.Authors,
                    opt => opt.MapFrom(
                        lit => lit.AuthorLiterature.Select(authLit => authLit.Author)
                        )
                    )
                .ForMember(
                    destinationMember => destinationMember.Annotations,
                    opt => opt.MapFrom(
                        lit => lit.AnnotationLiterature.Select(authLit => authLit .Annotation)
                        )
                    )
                ;

            CreateMap<LiteratureDto, Literature>()
                .ForMember(
                    literatureEntity => literatureEntity.AnnotationLiterature,
                    opt => opt.ResolveUsing<LiteratureDtoAnnotationToLiteratureAnnotationLiteratureResolver>()
                    )
                .ForMember(
                    literatureEntity => literatureEntity.AuthorLiterature,
                    opt => opt.ResolveUsing<LiteratureDtoAuthorToLiteratureAuthorLiteratureResolver>()
                    )
                ;
        }
    }
}