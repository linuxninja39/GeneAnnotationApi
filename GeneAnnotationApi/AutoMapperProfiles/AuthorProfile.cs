using System.Linq;
using AutoMapper;
using GeneAnnotationApi.AutoMapperProfiles.CustomResolvers;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(
                    authorDto => authorDto.Annotations,
                    opt => opt.MapFrom(
                        authorEntity => authorEntity.AnnotationAuthor.Select(
                            annotationAuthorEntity => annotationAuthorEntity.Annotation
                        )
                    )
                )
                .ForMember(
                    authorDto => authorDto.Literatures,
                    opt => opt.MapFrom(
                        authorEntity => authorEntity.AuthorLiterature.Select(
                            authorLiterature => authorLiterature.Literature
                        )
                    )
                )
                ;
            CreateMap<AuthorDto, Author>()
                .ForMember(
                    authorEntity => authorEntity.AnnotationAuthor,
                    opt => opt.ResolveUsing<AuthorDtoAnnotationToAuthorAnnotationAuthorResolver>()
                    )
                .ForMember(authorEntity => authorEntity.AuthorLiterature, opt => opt.Ignore())
                ;
        }
    }
}