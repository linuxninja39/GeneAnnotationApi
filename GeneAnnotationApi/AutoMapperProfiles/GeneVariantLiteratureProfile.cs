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
                    geneVariantLiteratureDto => geneVariantLiteratureDto.Annotations,
                    opt => opt.MapFrom(
                        geneVariantLiterature => geneVariantLiterature.AnnotationGeneVariantLiterature.Select(
                            annotationGeneVariantLiterature => annotationGeneVariantLiterature.Annotation
                        )
                    )
                )
                ;

            CreateMap<GeneVariantLiteratureDto, GeneVariantLiterature>()
                .ForMember(
                    geneVariantLiteratureEntity => geneVariantLiteratureEntity.AnnotationGeneVariantLiterature,
                    opt => opt.Ignore()
                    )
                .ForMember(
                    geneVariantLiteratureEntity => geneVariantLiteratureEntity.GeneVarLitDisorder,
                    opt => opt.Ignore()
                    )
                ;
        }
    }
}