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
                    destinationMember => destinationMember.Literature,
                    opt => opt.MapFrom(
                        geneVariant => geneVariant.GeneVariantLiterature.Select(
                            literatureGeneVariant => literatureGeneVariant.Literature
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