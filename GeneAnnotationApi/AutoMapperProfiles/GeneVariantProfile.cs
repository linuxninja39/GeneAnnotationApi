using System.Linq;
using AutoMapper;
using GeneAnnotationApi.AutoMapperProfiles.CustomResolvers;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneVariantProfile: Profile
    {
        public GeneVariantProfile()
        {
            CreateMap<GeneVariant, GeneVariantDto>()
                .ForMember(
                    destinationMember => destinationMember.Annotation,
                    opt => opt.MapFrom(
                            geneVariant =>
                                geneVariant.AnnotationGeneVariant.Select(
                                    annotationGeneVariant => annotationGeneVariant.Annotation
                                )
                        )
                )
                .ForMember(
                    geneVariantDto => geneVariantDto.CallTypes,
                    opt => opt.MapFrom(
                            geneVariantEntity =>
                                geneVariantEntity.CallTypeGeneVariants.Select(
                                    callTypeGeneVariant => callTypeGeneVariant.CallType
                                )
                        )
                )
                ;

            CreateMap<GeneVariantDto, GeneVariant>()
                .ForMember(
                    geneVariant => geneVariant.AnnotationGeneVariant,
                    opt => opt.ResolveUsing<GeneVariantDtoAnnotationToGeneVariantAnnotationGeneVariantResolver>()
                    )
                .ForMember(geneVariantEntity => geneVariantEntity.CallTypeGeneVariants, opt => opt.Ignore())
                ;
        }
    }
}