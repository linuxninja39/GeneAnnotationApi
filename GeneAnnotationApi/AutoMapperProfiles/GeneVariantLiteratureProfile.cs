using System.Linq;
using AutoMapper;
using GeneAnnotationApi.AutoMapperProfiles.CustomResolvers;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneVariantLiteratureProfile : Profile
    {
        public GeneVariantLiteratureProfile()
        {
            CreateMap<GeneVariantLiterature, GeneVariantLiteratureDto>()
                .ForMember(
                    geneVariantLiteratureDto => geneVariantLiteratureDto.Annotation,
                    opt => opt.MapFrom(
                        geneVariantLiterature => geneVariantLiterature.AnnotationGeneVariantLiterature.Select(
                            annotationGeneVariantLiterature => annotationGeneVariantLiterature.Annotation
                        )
                    )
                )
                ;

            CreateMap<GeneVariantLiteratureDto, GeneVariantLiterature>();
        }
    }
}