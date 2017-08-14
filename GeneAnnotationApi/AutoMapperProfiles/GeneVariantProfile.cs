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
                ;

            CreateMap<GeneVariantDto, GeneVariant>()
                .ForMember(
                    geneVariant => geneVariant.AnnotationGeneVariant,
                    opt => opt.ResolveUsing<GeneVariantDtoAnnotationToGeneVariantAnnotationGeneVariantResolver>()
                    )
                ;
        }
    }
}